import { Component, EventEmitter, Output, OnInit } from '@angular/core';

import { SelectItem } from 'primeng-lts/api';

import { IPeripheralDevice } from '../../models/peripheral-device';

import { generateInvoiceStatusOptions } from '../../utils';

@Component({
    selector: 'peripheral-device-edit-dialog',
    templateUrl: './peripheral-device-edit-dialog.component.html'
})
export class PeripheralDeviceEditDialogComponent implements OnInit {
    _visible = true;
    item: IPeripheralDevice = <IPeripheralDevice>{};

    statusOptions: SelectItem[] = [];

    @Output() saved = new EventEmitter<IPeripheralDevice>();
    @Output() closed = new EventEmitter<void>();

    ngOnInit(): void {
        this.initializeStatusOptions();
    }

    save(): void {
        this.saved.emit(this.item);
    }

    close(): void {
        this.closed.emit();
    }

    private initializeStatusOptions(): void {
        this.statusOptions = generateInvoiceStatusOptions();
    }
}