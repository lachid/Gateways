import { Component, OnInit } from '@angular/core';

import { ConfirmationService } from 'primeng-lts/api';

import { GatewayService } from '../../services/gateway.service';

import { IGateway } from '../../models/gateway';
import { IPeripheralDevice } from '../../models/peripheral-device';

@Component({
    selector: 'gateway-list',
    templateUrl: './gateway-list.component.html'
})
export class GatewayListComponent implements OnInit {
    _creating = false;
    _viewingItem: IGateway | undefined;

    gateways: IGateway[] = [];
    maxDevicesAllowed: number | undefined;

    constructor(private confirmationService: ConfirmationService, private gatewayService: GatewayService) { }

    ngOnInit(): void {
        this.initializeGateways();
        this.initializeMaxDevicesAllowed();
    }

    create(): void {
        this.startCreating();
    }

    view(item: IGateway): void {
        this.startViewing(item);
    }

    remove(id: number): void {
        this.confirmationService.confirm({
            message: "This item will be removed, do you want to continue?",
            accept: () => {
                this.gatewayService.remove(id)
                    .then(() => this.removeItem(id));
            }
        });
    }

    onEditDialogSaved(item: IGateway): void {
        this.addItem(item);
        this.stopCreating();
    }

    onEditDialogClosed(): void {
        this.stopCreating();
    }

    onDetailsDialogClosed(): void {
        this.stopViewing();
    }

    addDevice(id: number, device: IPeripheralDevice): void {
        this.gatewayService.addDevice(id, device)
            .then(savedDevice => this.addDeviceItem(id, savedDevice));
    }

    removeDevice(id: number, deviceId: number): void {
        this.gatewayService.removeDevice(id, deviceId)
            .then(() => this.removeDeviceItem(id, deviceId));
    }

    private initializeGateways(): void {
        this.gatewayService.getAll()
            .then(gateways => {
                this.gateways = gateways;
            });
    }

    private initializeMaxDevicesAllowed(): void {
        this.gatewayService.maxDevicesAllowed()
            .then(maxDevices => {
                this.maxDevicesAllowed = maxDevices;
            });
    }

    private startCreating(): void {
        this._creating = true;
    }

    private stopCreating(): void {
        this._creating = false;
    }

    private startViewing(item: IGateway): void {
        this._viewingItem = item;
    }

    private stopViewing(): void {
        this._viewingItem = undefined;
    }

    private addItem(item: IGateway): void {
        this.gateways = [...this.gateways, item];
    }

    private removeItem(id: number): void {
        this.gateways = this.gateways.filter(g => g.id !== id);
    }

    private addDeviceItem(gatewayId: number, device: IPeripheralDevice): void {
        const gateway = this.gateways.find(g => g.id === gatewayId);
        if (gateway) {
            gateway.devices = [...gateway.devices, device];
            this.resetGateways();
        }
    }

    private removeDeviceItem(gatewayId: number, deviceId: number): void {
        const gateway = this.gateways.find(g => g.id === gatewayId);
        if (gateway) {
            gateway.devices = gateway.devices.filter(d => d.id !== deviceId);
            this.resetGateways();
        }
    }

    private resetGateways(): void {
        this.gateways = [...this.gateways];
    }
}