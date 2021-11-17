import { Routes } from '@angular/router';

export const featureRoutes: Routes = [
    { path: 'gateways', loadChildren: () => import('./gateways/gateways.module').then(m => m.GatewaysModule) }
];