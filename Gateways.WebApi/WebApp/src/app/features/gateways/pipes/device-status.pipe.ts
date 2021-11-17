import { Pipe, PipeTransform } from '@angular/core';

import { PeripheralDeviceStatus } from '../models/peripheral-device-status';

@Pipe({name: 'deviceStatus'})
export class DeviceStatusPipe implements PipeTransform {
    transform(value: PeripheralDeviceStatus): string {
        return PeripheralDeviceStatus[value];
    }
}