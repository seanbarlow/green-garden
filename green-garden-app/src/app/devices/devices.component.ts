import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { Device } from '../models/device.model';
import { DevicesService } from '../services/devices.service';

@Component({
  selector: 'gg-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.sass']
})
export class DevicesComponent implements OnInit {
  public deviceId: number = 0;
  public devices: Device[];
  public showSensors: boolean = false;

  constructor(private deviceService: DevicesService, private router: Router) { }

  ngOnInit(): void {
    this.deviceService
      .findAll()
      .subscribe(data => {
        this.devices = data;
      });
  }

  public newDevice(): void {
    var url = `Devices/New`;
    this.router
      .navigateByUrl(url)
      .then(e => {
        if (e) {
          console.log("Navigation is successful!");
        } else {
          console.log("Navigation has failed!");
        }
      });
  }

  public viewSensors(id: number) {
    this.deviceId = id;
    this.showSensors = true;
  }
}
