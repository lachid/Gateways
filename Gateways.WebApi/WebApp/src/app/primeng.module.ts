import { NgModule } from '@angular/core';

import { ToastModule } from 'primeng-lts/toast';
import { MenubarModule } from 'primeng-lts/menubar';
import { TabViewModule } from 'primeng-lts/tabview';
import { TableModule } from 'primeng-lts/table';
import { DialogModule } from 'primeng-lts/dialog';
import { ButtonModule } from 'primeng-lts/button';
import { InputTextModule } from 'primeng-lts/inputtext';
import { DropdownModule } from 'primeng-lts/dropdown';
import { ConfirmDialogModule } from 'primeng-lts/confirmdialog';
import { DividerModule } from 'primeng-lts/divider';

@NgModule({
    imports: [
        ToastModule,
        MenubarModule,
        TabViewModule,
        TableModule,
        DialogModule,
        ButtonModule,
        InputTextModule,
        DropdownModule,
        ConfirmDialogModule,
        DividerModule
    ],
    declarations: [],
    exports: [
        ToastModule,
        MenubarModule,
        TabViewModule,
        TableModule,
        DialogModule,
        ButtonModule,
        InputTextModule,
        DropdownModule,
        ConfirmDialogModule,
        DividerModule
    ]
})
export class PrimeNgModule { }