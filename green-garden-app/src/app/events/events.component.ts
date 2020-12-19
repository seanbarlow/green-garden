import { Component, OnInit } from '@angular/core';
import { DeviceEvent } from '../models/device-event.model';
import { EventsService } from '../services/events.service';

@Component({
  selector: 'gg-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.sass']
})
export class EventsComponent implements OnInit {
public events: DeviceEvent[];
public devices: string[];
  constructor(private eventService: EventsService) { }

  ngOnInit(): void {
    this.eventService
    .findAll()
    .subscribe(events => {
      this.events = events
    });
  }
}
