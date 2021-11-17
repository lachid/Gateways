export const enum PeripheralDeviceStatus {
    Offline = 0,
    Online = 1
}

export interface IPeripheralDevice {
    id: number;
    gatewayId: number;
    vendor: number;
    dateCreated: Date;
    status: PeripheralDeviceStatus
}