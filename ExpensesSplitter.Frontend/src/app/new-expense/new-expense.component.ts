import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
    amount: 0.0
  }

  constructor(
    private readonly expensesService: ExpensesService,
    private readonly router: Router,
    activatedRoute: ActivatedRoute
  ) {
    this.settlementId = activatedRoute.snapshot.params.settlementId;
   }

  ngOnInit(): void {
  }

  save(expense: NewExpense) {
    this.expensesService.addExpense(this.settlementId, expense).subscribe({
      next: id => this.router.navigateByUrl(`../${id}`)
    });
  }

}
