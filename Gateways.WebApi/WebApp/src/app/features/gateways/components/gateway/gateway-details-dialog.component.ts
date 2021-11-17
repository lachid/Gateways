import { Component, EventEmitter, Input, Output } from '@angular/core';

import { IGateway } from '../../models/gateway';

@Component({
    selector: 'gateway-details-dialog',
    templateUrl: './gateway-details-dialog.component.html',
    styleUrls: ['./gateway-details-dialog.component.css']
})
export class GatewayDetailsDialogComponent {
    _visible = true;
    
    @Input() item: IGateway | undefined;

    @Output() closed = new EventEmitter<void>();

    close(): void {
        this.closed.emit();
    }
}