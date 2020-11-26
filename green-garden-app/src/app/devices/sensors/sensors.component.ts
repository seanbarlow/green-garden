import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Sensor } from '../../models/sensor.model';
import { SensorsService } from '../../services/sensors.service';

@Component({
  selector: 'gg-sensors',
  templateUrl: './sensors.component.html',
  styleUrls: ['./sensors.component.sass']
})
export class SensorsComponent implements OnInit, OnDestroy {
  @Input() deviceId: number;
  private sensorsSubscription: Subscription;
  private deleteSubscription: Subscription;

  public sensors: Sensor[] = [];

  constructor(public sensorService: SensorsService) { }

  ngOnDestroy(): void {
    this.sensorsSubscription.unsubscribe();
    this.deleteSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.sensorService.configureUrl(this.deviceId);
    this.updateSensors();
  }

  public newSensorHandler(sensor: Sensor): void {
    this.sensors.push(sensor);
  }

  public deleteSensor(id: number): void {
    this.deleteSubscription = this.sensorService
      .delete(id)
      .subscribe(() =>
        this.updateSensors()
      )
  }

  private updateSensors(): void {
    this.sensorsSubscription = this.sensorService.findSensors()
      .subscribe(data => this.sensors = data);
  }
}
