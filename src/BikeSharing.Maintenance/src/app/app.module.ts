import { NgModule } from '@angular/core';
import { IonicApp, IonicModule } from 'ionic-angular';
import { BikeSharingApp } from './app.component';
import { ReportsPage, ReportsList, ReportsDetail, ReportsConfirm } from '../pages/reports';
import { TabsPage } from '../pages/tabs/tabs';
import { AuthService } from './app.auth';
import { ReportsService, DistanceService } from '../shared/reports';

@NgModule({
    declarations: [
        BikeSharingApp,
        ReportsPage,
        TabsPage,
        ReportsList,
        ReportsDetail,
        ReportsConfirm
    ],
    imports: [
        IonicModule.forRoot(BikeSharingApp)
    ],
    bootstrap: [IonicApp],
    entryComponents: [
        BikeSharingApp,
        ReportsPage,
        TabsPage,
        ReportsList,
        ReportsDetail,
        ReportsConfirm
    ],
    providers: [
        AuthService,
        DistanceService,
        ReportsService
    ]
})
export class AppModule { }
