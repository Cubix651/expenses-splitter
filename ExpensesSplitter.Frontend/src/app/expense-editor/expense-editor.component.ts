import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NewExpense } from '../models/expenses.model';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-expense-editor',
  templateUrl: './expense-editor.component.html',
  styleUrls: ['./expense-editor.component.scss']
})
export class ExpenseEditorComponent implements OnInit {
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
