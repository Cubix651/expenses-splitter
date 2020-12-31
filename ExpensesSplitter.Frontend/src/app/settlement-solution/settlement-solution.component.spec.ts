import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SettlementSolutionComponent } from './settlement-solution.component';

describe('SettlementSolutionComponent', () => {
  let component: SettlementSolutionComponent;
  let fixture: ComponentFixture<SettlementSolutionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SettlementSolutionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SettlementSolutionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
