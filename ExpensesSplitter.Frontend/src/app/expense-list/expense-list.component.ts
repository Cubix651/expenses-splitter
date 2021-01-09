import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpensesService } from '../services/expenses.service';
import { Expense } from '../models/expenses.model';
import { SettlementUser } from '../models/settlement-user.model';

@Component({
  selector: 'app-expense-list',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.scss'],
  providers: [ExpensesService]
})
export class ExpenseListComponent implements OnInit {
  settlementId: string;
  loadErrorOccurred: boolean = false;
  deleteErrorOccurred: boolean = false;
  expenses: Expense[];

  constructor(
    private readonly service: ExpensesService,
    route: ActivatedRoute,
  ) {
    this.settlementId = route.snapshot.params.settlementId;
  }


  ngOnInit(): void {
    this.fetchExpenses();
  }

  private fetchExpenses() {
    this.service.getExpenses(this.settlementId)
      .subscribe({
        next: expenses => this.expenses = expenses,
        error: error => {
          console.error('Error during fetching expenses', error);
          this.loadErrorOccurred = true;
        }
      });
  }

  deleteExpense(expenseId: string) {
    this.deleteErrorOccurred = false;

    this.service.deleteExpense(this.settlementId, expenseId)
      .subscribe({
        complete: () => this.fetchExpenses(),
        error: error => {
          console.error('Error during removing expense', error);
          this.deleteErrorOccurred = true;
        }
      });
  }

  trackById(index: number, expense: Expense) {
    return expense.id;
  }

  participantsInfo(expense: Expense): string {
    const participants = expense.participants;
    if (participants == null || participants.length == 0) {
      return "wszyscy";
    } else {
      return participants.length.toString();
    }
  }

  participantsList(expense: Expense): string {
    const participants = expense.participants;
    if (participants == null || participants.length == 0) {
      return null;
    } else {
      return participants.map(p => p.displayName).join(', ');
    }
  }
}
