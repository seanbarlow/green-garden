import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { DevicesComponent } from './devices/devices.component';
import { LogsComponent } from './logs/logs.component';
import { NewDeviceComponent } from './devices/new-device/new-device.component';
import { LookupTypesComponent } from './lookup-types/lookup-types.component';
import { LookupsComponent } from './lookups/lookups.component';
import { SensorsComponent } from './devices/sensors/sensors.component';
import { AddSensorComponent } from './devices/add-sensor/add-sensor.component';
import { EventsComponent } from './events/events.component';

@NgModule({
  declarations: [
    AppComponent,
    DevicesComponent,
    LogsComponent,
    NewDeviceComponent,
    LookupTypesComponent,
    LookupsComponent,
    SensorsComponent,
    AddSensorComponent,
    EventsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
