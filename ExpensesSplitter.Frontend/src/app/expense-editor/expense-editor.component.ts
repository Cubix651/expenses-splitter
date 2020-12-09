import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NewExpense } from '../models/expenses.model';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { SettlementUser } from '../models/settlement-user.model';

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
      amount: [value.amount],
      whoPaidId: [value.whoPaidId]
    })
  }

  @Input()
  people: SettlementUser[] = [
    { id: '12345422-0000-0000-0000-000000000000', displayName: 'Inna'},
    { id: '00000000-0000-0000-0000-000000000000', displayName: 'Bezimienny'}
  ];

  get expense() : NewExpense {
    return this.form.value
  }

  form: FormGroup;

  constructor(
    private readonly fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }

  onChangeWhoPaid(newValue: string) {
    this.form.get('whoPaidId').setValue(newValue);
  }
}
