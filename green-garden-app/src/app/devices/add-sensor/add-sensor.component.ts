import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { Sensor } from '../../models/sensor.model';
import { LookupTypeService } from '../../services/lookup-type.service';
import { SensorsService } from '../../services/sensors.service';
import { Lookup } from '../../models/lookup.model';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'gg-add-sensor',
  templateUrl: './add-sensor.component.html',
  styleUrls: ['./add-sensor.component.sass']
})
export class AddSensorComponent implements OnInit, OnChanges, OnDestroy {
  @Input() deviceId: number;
  @Output() newSensor: EventEmitter<Sensor> = new EventEmitter();
  @ViewChild('f') form: any;

  public model: Sensor;
  public sensorTypes: Lookup[] = [];

  private sensorTypesSubscription: Subscription;

  constructor(private sensorsService: SensorsService,
    private lookupTypeService: LookupTypeService) { }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.deviceId) {
      for (let property in changes) {
        if (property === 'deviceId') {
          console.log('Previous:', changes[property].previousValue);
          console.log('Current:', changes[property].currentValue);
          console.log('firstChange:', changes[property].firstChange);
        }
      }
      this.sensorsService.configureUrl(changes.deviceId.currentValue);
      this.model = new Sensor(0, changes.deviceId.currentValue, 0, '', new Date());
      // dispatch action to load the details here.
    }
  }

  ngOnInit(): void {
    this.sensorTypesSubscription = this.lookupTypeService
      .findAllByUniqueId('sensortypes')
      .subscribe(data => 
        {
          this.sensorTypes = data;
          this.form.reset();
        });
    
  }

  ngOnDestroy(): void {
    this.sensorTypesSubscription.unsubscribe();
  }

  public cancel(): void {
    this.resetSensor();
    this.form.reset();
  }

  public onSubmit(): void {
    this.sensorsService
      .save(this.model).subscribe(result => {
        this.newSensor.emit(result);
        this.form.reset();
      });
  }

  private resetSensor(): void {
    this.model = new Sensor(0, this.deviceId, 0, '', new Date());
  }
}
