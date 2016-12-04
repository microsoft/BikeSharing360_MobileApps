import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Loading, LoadingController } from 'ionic-angular';
import { Observable } from 'rxjs/Rx';
import { Report } from './index';
import Config from '../../app/app.config';


@Injectable()
export class ReportsService {
    private baseUrl: string = Config.apiUrl;
    private loading: Loading;
    private loadingOpts: any;

    constructor(private http: Http,
                private loadingCtrl: LoadingController) {

        this.loadingOpts = {
            content: 'Please wait...',
            dismissOnPageChange: false
        };
    }

    public getReports(): Observable<Report[]> {
        this.loading = this.loadingCtrl.create(this.loadingOpts);
        this.loading.present();

        return this.http.get(this.baseUrl + '/issues/from/' + Config.userId)
            .map(x => this.extractData(x))
            .catch(x => this.handleError(x));
    }

    public getReportDetail(id: number): Observable<Report> {
        this.loading = this.loadingCtrl.create(this.loadingOpts);
        this.loading.present();

        return this.http.get(this.baseUrl + '/issues/' + id)
            .map(x => this.extractData(x))
            .catch(x => this.handleError(x));
    }

    public markAsSolved(report: any): Observable<any> {
        this.loading = this.loadingCtrl.create(this.loadingOpts);
        this.loading.present();

        return this.http.put(this.baseUrl + '/issues/solved/' + report.id, {})
            .map(x => this.extractData(x))
            .catch(x => this.handleError(x));
    }

    public markAsUnsolved(report: any): Observable<any> {
        this.loading = this.loadingCtrl.create(this.loadingOpts);
        this.loading.present();

        return this.http.delete(this.baseUrl + '/issues/solved/' + report.id, {})
            .map(x => this.extractData(x))
            .catch(x => this.handleError(x));
    }

    private extractData(res: Response): any[] {
        this.loading.dismiss();

        if (res.text().length < 1) {
            return [];
        }

        return res.json();
    }

    private handleError(error: any): Observable<any> {
        this.loading.dismiss();
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg);
        return Observable.throw(errMsg);
    }

}
