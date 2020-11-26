import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupTypesComponent } from './lookup-types.component';

describe('LookupTypesComponent', () => {
  let component: LookupTypesComponent;
  let fixture: ComponentFixture<LookupTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LookupTypesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
