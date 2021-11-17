import { Component } from '@angular/core';

import { MenuItem, PrimeIcons } from 'primeng-lts/api';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html'
})
export class LayoutComponent {
    menuItems: MenuItem[] = [];

    constructor() {
        this.initializeMenu();
    }

    private initializeMenu(): void {
        this.menuItems = [
            {
                label: 'Gateways',
                icon: PrimeIcons.LIST,
                routerLink: 'gateways'
            }
        ];
    }
}