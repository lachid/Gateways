import { SelectItem } from "primeng-lts/api";

import { PeripheralDeviceStatus } from "./models/peripheral-device-status";

const PeripheralDeviceStatusValues = Object.values(PeripheralDeviceStatus)
    .filter(s => !isNaN(Number(s)))
    .map(s => <PeripheralDeviceStatus>s);

export const generateInvoiceStatusOptions = (): SelectItem[] =>
    PeripheralDeviceStatusValues.map(s => ({ value: s, label: PeripheralDeviceStatus[s] } as SelectItem));