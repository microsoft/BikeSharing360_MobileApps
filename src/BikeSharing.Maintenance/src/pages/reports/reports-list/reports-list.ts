import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Report, ReportsService, DistanceService } from '../../../shared/reports';
import { ReportsDetail } from './../';

@Component({
    selector: 'bs-reports_list .bs-reports_list',
    templateUrl: 'reports-list.html'
})
export class ReportsList {
    public reports: Report[];
    private waiting: number = 0;

    constructor(private navCtrl: NavController,
            private reportsService: ReportsService,
            private distanceService: DistanceService) {

        this.retrieveData();
    }

    private retrieveData(): void {
        this.reportsService.getReports().subscribe((reports: Report[]) => {
            this.reports = reports;
            this.getWaiting();
            this.geolocate();
        });
    }

    private geolocate(): void {
        this.distanceService.geolocate()
            .then(() => this.reports.map(x => this.distanceService.calculateDistance(x)));
    }

    private getWaiting(): void {
        this.waiting = 0;
        this.reports
            .map(report => {
                if (report.solved) {
                    return;
                }

                this.waiting++;
            });
    }

    public itemTapped(event, report): void {
        this.reportsService.getReportDetail(report.id).subscribe((newReport: Report) => {
            newReport.distance = report.distance;
            this.navCtrl.push(ReportsDetail, {
                report: newReport
            });
        });
    }
}
