import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { AuthService } from './../../app/app.auth';
import { ReportsList } from './';
import Config from '../../app/app.config';

@Component({
    selector: 'bs-reports .bs-reports',
    templateUrl: 'reports.html'
})
export class ReportsPage {
    private failed: boolean = false;

    constructor(private auth: AuthService,
                private navCtrl: NavController) {

        if (!Config.loginRequired) {
            this.navCtrl.setRoot(ReportsList);
            return;
        }
        setTimeout(() => this.login(), 500);
    }

    public goToReports(): void {
        this.navCtrl.setRoot(ReportsList);
    }

    private login(): void {
        this.auth.logIn()
            .then((response) => {
                this.failed = false;
                setTimeout(() => this.navCtrl.setRoot(ReportsList), 10);
            })
            .catch((err) => {
                this.failed = true;
            });
    }

}
