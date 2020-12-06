import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NewExpense } from '../models/expenses.model';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-expense-details',
  templateUrl: './expense-details.component.html',
  styleUrls: ['./expense-details.component.scss']
})
export class ExpenseDetailsComponent implements OnInit {
  @Input() expense: NewExpense;
  @Output() onSaveClick: EventEmitter<NewExpense> = new EventEmitter();

  form: FormGroup;

  constructor(
    private readonly fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      name: [this.expense.name],
      description: [this.expense.description],
      amount: [this.expense.amount]
    })
  }

  save() {
    this.onSaveClick.emit(this.form.value)
  }

}
