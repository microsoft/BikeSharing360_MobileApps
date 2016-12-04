import { Component } from '@angular/core';
import { Platform } from 'ionic-angular';
import { StatusBar } from 'ionic-native';

import { TabsPage } from '../pages/tabs';

@Component({
    template: `<ion-nav [root]="rootPage"></ion-nav>`
})
export class BikeSharingApp {
    public rootPage = TabsPage;

    constructor(private platform: Platform) {
        platform.ready().then(() => {
            StatusBar.overlaysWebView(false);
            StatusBar.backgroundColorByHexString('#1D3B93'); // set status bar to white
        });
    }
}
