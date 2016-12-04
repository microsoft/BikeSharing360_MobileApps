import { Component } from '@angular/core';
import { Config, Platform } from 'ionic-angular';
import { ReportsPage } from '../reports';

@Component({
    selector: 'bs-tabs .bs-tabs',
    templateUrl: 'tabs.html'
})
export class TabsPage {
    public tab1Root: Component = ReportsPage;
    public needTabsWithIcons: boolean;

    constructor(private config: Config,
                private platform: Platform) {

        this.config.set('tabsPlacement', 'top');
        this.config.set('backButtonText', '');
        this.config.set('backButtonIcon', 'bs-arrow');
        this.config.set('tabsHideOnSubPages', true);

        this.needTabsWithIcons = this.platform.is('ios');
    }
}
