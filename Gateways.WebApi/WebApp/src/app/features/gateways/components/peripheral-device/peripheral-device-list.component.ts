import { Component, EventEmitter, Input, Output } from '@angular/core';

import { ConfirmationService } from 'primeng-lts/api';

import { IPeripheralDevice } from '../../models/peripheral-device';

@Component({
    selector: 'peripheral-device-list',
    templateUrl: './peripheral-device-list.component.html'
})
export class PeripheralDeviceListComponent {
    _creating = false;

    get canAdd(): boolean { return this.maxDevices === undefined || this.devices.length < this.maxDevices; }

    @Input() devices: IPeripheralDevice[] = [];
    @Input() maxDevices: number | undefined;

    @Output() saved = new EventEmitter<IPeripheralDevice>();
    @Output() removed = new EventEmitter<number>();

    constructor(private confirmationService: ConfirmationService) { }

    create(): void {
        this.startCreating();
    }

    remove(id: number): void {
        this.confirmationService.confirm({
            message: "This item will be removed, do you want to continue?",
            accept: () => {
                this.removed.emit(id);
            }
        });
    }

    onSaved(item: IPeripheralDevice): void {
        this.saved.emit(item);
        this.stopCreating();
    }

    onClosed(): void {
        this.stopCreating();
    }

    private startCreating(): void {
        this._creating = true;
    }

    private stopCreating(): void {
        this._creating = false;
    }
}