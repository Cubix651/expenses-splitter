import { TestBed } from '@angular/core/testing';

import { SettlementSolutionService } from './settlement-solution.service';

describe('SettlementSolutionService', () => {
  let service: SettlementSolutionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SettlementSolutionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
