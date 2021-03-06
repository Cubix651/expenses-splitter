import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseEditorComponent } from '../expense-editor/expense-editor.component';
import { NewExpense } from '../models/expenses.model';
import { ExpensesService } from '../services/expenses.service';

@Component({
  selector: 'app-new-expense',
  templateUrl: './new-expense.component.html',
  styleUrls: ['./new-expense.component.scss'],
  providers: [ExpensesService]
})
export class NewExpenseComponent implements OnInit {
  settlementId: string;
  expense: NewExpense = {
    name: '',
    description: '',
    dateTime: new Date(),
    amount: 0.0,
    whoPaidId: null,
    participants: [],
  }
  isSaveInProgress: boolean = false;
  saveErrorOccurred: boolean = false;

  @ViewChild('editor') editor: ExpenseEditorComponent;

  constructor(
    private readonly expensesService: ExpensesService,
    private readonly router: Router,
    private readonly activatedRoute: ActivatedRoute
  ) {
    this.settlementId = activatedRoute.snapshot.params.settlementId;
  }

  ngOnInit(): void {
  }

  create(expense: NewExpense) {
    this.isSaveInProgress = true;
    this.saveErrorOccurred = false;

    this.expensesService.addExpense(this.settlementId, expense).subscribe({
      next: id => this.router.navigate([`..`, id], { relativeTo: this.activatedRoute }),
      error: error => {
        console.error('Error during creating expense', error);
        this.saveErrorOccurred = true;
        this.isSaveInProgress = false;
      }
    });
  }

}
