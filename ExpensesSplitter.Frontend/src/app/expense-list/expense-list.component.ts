import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpensesService } from '../services/expenses.service';
import { map, switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Expense } from './expense.model';

@Component({
  selector: 'app-expense-list',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.scss'],
  providers: [ExpensesService]
})
export class ExpenseListComponent implements OnInit {

  constructor(
    private readonly service: ExpensesService,
    private readonly route: ActivatedRoute,
    ) { }
    
  expenses$: Observable<Expense[]>;
  
  ngOnInit(): void {
    this.expenses$ = this.route.params.pipe(
      map(params => params['settlementId']),
      switchMap(settlementId => this.service.getExpenses(settlementId))
    );
  }

}
