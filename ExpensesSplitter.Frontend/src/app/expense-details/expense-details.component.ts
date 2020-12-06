import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ExpenseEditorComponent } from '../expense-editor/expense-editor.component';
import { Expense, NewExpense } from '../models/expenses.model';
import { ExpensesService } from '../services/expenses.service';

@Component({
  selector: 'app-expense-details',
  templateUrl: './expense-details.component.html',
  styleUrls: ['./expense-details.component.scss'],
  providers: [ExpensesService]
})
export class ExpenseDetailsComponent implements OnInit {
  settlementId: string;
  expenseId: string;

  expense$: Observable<Expense>;

  @ViewChild('editor') editor: ExpenseEditorComponent;

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
