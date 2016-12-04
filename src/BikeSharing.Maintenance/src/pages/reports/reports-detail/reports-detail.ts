import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Report, ReportsService, ReportBike } from '../../../shared/reports';
import { ReportsConfirm } from './../';
import { PopoverController } from 'ionic-angular';

@Component({
    selector: 'bs-reports_detail .bs-reports_details',
    templateUrl: 'reports-detail.html'
})
export class ReportsDetail {
    private report: Report;
    private distance: number;
    private bikeInfo: ReportBike;

    constructor(private navCtrl: NavController,
                private navParams: NavParams,
                private popoverCtrl: PopoverController,
                private reportsService: ReportsService) {

        this.report = navParams.get('report');
        this.distance = this.report.distance;
        this.bikeInfo = this.report.bikeInfo;
    }

    public markAsSolved(): void {
        this.reportsService.markAsSolved(this.report).subscribe(() => {
            this.navCtrl.push(ReportsConfirm, {
                report: this.report
            });
        });
    }

    public getTotalIncidences(): number {
        return this.bikeInfo.totalIncidences > 0 ? this.bikeInfo.totalIncidences : this.report.incidences.length + 1;
    }
}
