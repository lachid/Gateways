import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { PrimeNgModule } from 'src/app/primeng.module';

import { GatewayListComponent } from './components/gateway/gateway-list.component';
import { GatewayEditDialogComponent } from './components/gateway/gateway-edit-dialog.component';
import { GatewayDetailsDialogComponent } from './components/gateway/gateway-details-dialog.component';
import { PeripheralDeviceListComponent } from './components/peripheral-device/peripheral-device-list.component';
import { PeripheralDeviceEditDialogComponent } from './components/peripheral-device/peripheral-device-edit-dialog.component';

import { GatewayService } from './services/gateway.service';

import { DeviceStatusPipe } from './pipes/device-status.pipe';

const routes: Routes = [
    { path: '', component: GatewayListComponent }
];

@NgModule({
    declarations: [
        GatewayListComponent, GatewayEditDialogComponent, PeripheralDeviceListComponent,
        PeripheralDeviceEditDialogComponent, GatewayDetailsDialogComponent,
        DeviceStatusPipe
    ],
    imports: [
        CommonModule, FormsModule, HttpClientModule,
        PrimeNgModule,
        RouterModule.forChild(routes)
    ],
    exports: [],
    providers: [GatewayService]
})
export class GatewaysModule { }