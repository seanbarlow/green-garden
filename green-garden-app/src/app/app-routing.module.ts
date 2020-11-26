import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DevicesComponent } from './devices/devices.component';
import { NewDeviceComponent } from './devices/new-device/new-device.component';
import { LogsComponent } from './logs/logs.component';

const routes: Routes = [
  { path: 'Devices', component: DevicesComponent },
  { path: 'Devices/New', component: NewDeviceComponent },
  { path: 'Logs', component: LogsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
