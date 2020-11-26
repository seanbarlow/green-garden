import { Component, OnInit } from '@angular/core';
import { Lookup } from '../../models/lookup.model';
import { DevicesService } from '../../services/devices.service';
import { Device } from '../../models/device.model';

@Component({
  selector: 'gg-new-device',
  templateUrl: './new-device.component.html',
  styleUrls: ['./new-device.component.sass']
})
export class NewDeviceComponent implements OnInit {
  model = new Device(0, '', 0);
  deviceTypes: Lookup[] = [];

  constructor(private devicesService: DevicesService){
  }

  ngOnInit(): void {

  }

  public onSubmit() {
    this.devicesService.save(this.model).subscribe( result => 
      console.log(`Device Saved: ${result.id}`)
    );
  }

  public newDevice(): void{
    this.model = new Device(0,'', 0);
  }
}

