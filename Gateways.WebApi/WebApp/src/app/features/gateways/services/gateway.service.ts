import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MessageService } from 'primeng-lts/api';

import { ApiService } from 'src/app/main/services/api.service';

import { IGateway } from '../models/gateway';
import { IPeripheralDevice } from '../models/peripheral-device';

@Injectable()
export class GatewayService extends ApiService {
    url = "api/gateway";

    private _maxDevicesAllowed: number | undefined = undefined; 

    constructor(messageService: MessageService, private http: HttpClient) {
        super(messageService);
    }

    getAll(): Promise<IGateway[]> {
        return this.handleRequest(this.http.get<IGateway[]>(`${this.url}`));
    }

    create(item: IGateway): Promise<IGateway> {
        return this.handleRequest(this.http.post<IGateway>(`${this.url}`, item));
    }

    remove(id: number): Promise<void> {
        return this.handleRequest(this.http.delete<void>(`${this.url}/${id}`));
    }

    addDevice(id: number, device: IPeripheralDevice): Promise<IPeripheralDevice> {
        return this.handleRequest(this.http.post<IPeripheralDevice>(`${this.url}/${id}/peripheral-device`, device));
    }

    removeDevice(id: number, deviceId: number): Promise<void> {
        return this.handleRequest(this.http.delete<void>(`${this.url}/${id}/peripheral-device/${deviceId}`));
    }

    maxDevicesAllowed(): Promise<number> {
        if (this._maxDevicesAllowed != undefined) {
            return Promise.resolve(this._maxDevicesAllowed);
        }

        return this.handleRequest(this.http.get<number>(`${this.url}/max-devices-allowed`), false)
            .then(maxDevices => {
                this._maxDevicesAllowed = maxDevices;
                return maxDevices;
            });
    }
}