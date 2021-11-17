import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { PrimeNgModule } from '../primeng.module'

import { ConfirmationService, MessageService } from 'primeng-lts/api';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './components/app.component';
import { LayoutComponent } from './components/layout.component';
import { HomeComponent } from './components/home.component';

@NgModule({
    declarations: [
        AppComponent, LayoutComponent, HomeComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        PrimeNgModule
    ],
    providers: [MessageService, ConfirmationService],
    bootstrap: [AppComponent]
})
export class AppModule { }
