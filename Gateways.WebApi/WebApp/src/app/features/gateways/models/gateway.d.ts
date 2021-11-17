import { IPeripheralDevice } from "./peripheral-device";

export interface IGateway {
    id: number,
    serialNumber: string;
    name: string;
    ipAddressV4: string;
    devices: IPeripheralDevice[];
}