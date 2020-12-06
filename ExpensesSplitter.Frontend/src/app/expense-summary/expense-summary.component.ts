import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ExpenseDetailsComponent } from '../expense-details/expense-details.component';
import { Expense, NewExpense } from '../models/expenses.model';
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

  @ViewChild('editor') editor: ExpenseDetailsComponent;

  constructor(
    private readonly expensesService: ExpensesService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router
  ) {
    this.settlementId = activatedRoute.snapshot.params.settlementId;
    this.expenseId = activatedRoute.snapshot.params.expenseId;
  }


  ngOnInit(): void {
    this.expense$ = this.expensesService.getExpense(this.settlementId, this.expenseId);
  }

  save(expense: NewExpense) {
    this.expensesService.updateExpense(this.settlementId, this.expenseId, expense)
      .subscribe({
        complete: () => this.router.navigate(['../'], { relativeTo: this.activatedRoute })
      })
  }

}
