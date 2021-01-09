import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpenseEditorComponent } from '../expense-editor/expense-editor.component';
import { Expense, NewExpense } from '../models/expenses.model';
import { ExpensesService } from '../services/expenses.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-expense-details',
  templateUrl: './expense-details.component.html',
  styleUrls: ['./expense-details.component.scss'],
  providers: [ExpensesService]
})
export class ExpenseDetailsComponent implements OnInit {
  settlementId: string;
  expenseId: string;
  isSaveInProgress: boolean = false;
  isSaveFinishedSuccessfully: boolean = false;
  saveErrorOccurred: boolean = false
  isLoaded: boolean = false;
  loadErrorOccurred: boolean = false

  expense: NewExpense;

  @ViewChild('editor') editor: ExpenseEditorComponent;

  constructor(
    private readonly expensesService: ExpensesService,
    activatedRoute: ActivatedRoute
  ) {
    this.settlementId = activatedRoute.snapshot.params.settlementId;
    this.expenseId = activatedRoute.snapshot.params.expenseId;
  }

  ngOnInit(): void {
    this.expensesService.getExpense(this.settlementId, this.expenseId)
      .pipe(
        map<Expense, NewExpense>(e => ({
          ...e,
          whoPaidId: e.whoPaid.id,
          participants: e.participants.map(p => p.id),
        }))
      )
      .subscribe({
        next: expense => this.expense = expense,
        error: error => {
          console.error('Error during loading expense', error);
          this.loadErrorOccurred = true;
        },
        complete: () => this.isLoaded = true
      });
  }

  save(expense: NewExpense) {
    this.isSaveInProgress = true;
    this.isSaveFinishedSuccessfully = false;
    this.saveErrorOccurred = false;

    this.expensesService.updateExpense(this.settlementId, this.expenseId, expense)
      .subscribe({
        complete: () => {
          this.isSaveInProgress = false;
          this.isSaveFinishedSuccessfully = true;
        },
        error: error => {
          this.isSaveInProgress = false;
          this.saveErrorOccurred = true;
          console.error('Error during saving expense', error);
        }
      })
  }

}
