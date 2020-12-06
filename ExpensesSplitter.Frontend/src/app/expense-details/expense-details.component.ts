import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NewExpense } from '../models/expenses.model';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-expense-details',
  templateUrl: './expense-details.component.html',
  styleUrls: ['./expense-details.component.scss']
})
export class ExpenseDetailsComponent implements OnInit {
  @Input()
  set expense(value: NewExpense) {
    this.form = this.fb.group({
      name: [value.name],
      description: [value.description],
      amount: [value.amount]
    })
  }

  get expense() : NewExpense {
    return this.form.value
  }

  form: FormGroup;

  constructor(
    private readonly fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }

}
