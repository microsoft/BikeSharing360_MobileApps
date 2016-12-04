import { Injectable } from '@angular/core';
import { Report } from './index';
import { Geolocation, Geoposition } from 'ionic-native';

@Injectable()
export class DistanceService {
    private pos: Geoposition;

    public geolocate(): Promise<Geoposition> {
        return Geolocation.getCurrentPosition()
            .then((pos: Geoposition) => {
                this.pos = pos;
            });
    }

    public calculateDistance(report: Report): Report {
        report.distance = this.calculateDifference(this.pos.coords, report.station);
        return report;
    }

    private calculateDifference(pos1, pos2): number {
        const R = 6371; // km
        const toMile = 1.609;

        let dLat = this.toRad(pos2.latitude - pos1.latitude);
        let dLon = this.toRad(pos2.longitude - pos1.longitude);
        let lat1 = this.toRad(pos1.latitude);
        let lat2 = this.toRad(pos2.latitude);

        let a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
            Math.sin(dLon / 2) * Math.sin(dLon / 2) * Math.cos(lat1) * Math.cos(lat2);
        let c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        let d = R * c;
        return parseFloat((d * toMile).toFixed(2));
    }

    private toRad(Value): number {
        return Value * Math.PI / 180;
    }
}
