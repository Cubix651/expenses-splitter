import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Expense } from '../models/expenses.model';
import { ExpensesService } from '../services/expenses.service';

@Component({
  selector: 'app-expense-summary',
  templateUrl: './expense-summary.component.html',
  styleUrls: ['./expense-summary.component.scss'],
  providers: [ExpensesService]
})
export class ExpenseSummaryComponent implements OnInit {
  settlementId: string;
  expenseId: string;

  expense$: Observable<Expense>;

  constructor(
    private readonly expensesService: ExpensesService,
    activatedRoute: ActivatedRoute
  ) {
    this.settlementId = activatedRoute.snapshot.params.settlementId;
    this.expenseId = activatedRoute.snapshot.params.expenseId;
  }


  ngOnInit(): void {
    this.expense$ = this.expensesService.getExpense(this.settlementId, this.expenseId);
  }

}
