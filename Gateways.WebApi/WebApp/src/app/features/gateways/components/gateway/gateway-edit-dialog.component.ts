import { Component, EventEmitter, Output } from '@angular/core';

import { GatewayService } from '../../services/gateway.service';

import { IGateway } from '../../models/gateway';

@Component({
    selector: 'gateway-edit-dialog',
    templateUrl: './gateway-edit-dialog.component.html'
})
export class GatewayEditDialogComponent {
    _visible = true;
    _saving = false;
    item: IGateway = <IGateway>{};

    @Output() saved = new EventEmitter<IGateway>();
    @Output() closed = new EventEmitter<void>();

    constructor(private gatewayService: GatewayService) { }

    save(): void {
        this._saving = true;
        this.gatewayService.create(this.item)
            .then(gateway => {
                this.saved.emit(gateway);
                this._visible = false;
            })
            .finally(() => {
                this._saving = false;
            });
    }

    close(): void {
        this.closed.emit();
    }
}