import { TestBed } from '@angular/core/testing';

import { SettlementUsersService } from './settlement-users.service';

describe('SettlementUsersService', () => {
  let service: SettlementUsersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SettlementUsersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
