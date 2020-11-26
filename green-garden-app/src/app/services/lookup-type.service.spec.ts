import { TestBed } from '@angular/core/testing';

import { LookupTypeService } from './lookup-type.service';

describe('LookupTypeService', () => {
  let service: LookupTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LookupTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
