import { ReportStation, ReportBike } from './index';

export interface Report {
    type: string;
    id: number;
    description: string;
    station: ReportStation;
    time: Date;
    solved: boolean;
    distance: number;
    subtitle: string;
    bikeInfo: ReportBike;
    incidences: Report[];
}
