import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NewExpense } from '../models/expenses.model';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { SettlementUser } from '../models/settlement-user.model';

@Component({
  selector: 'app-settlementuser-editor',
  templateUrl: './settlementuser-editor.component.html',
  styleUrls: ['./settlementuser-editor.component.scss']
})
export class SettlementUserEditorComponent implements OnInit {
  @Output() id: string;
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
   // { id: '12345422-0000-0000-0000-000000000000', displayName: 'Inna'},
   // { id: '00000000-0000-0000-0000-000000000000', displayName: 'Bezimienny'}
    { id: 'e06fb564-fb6e-41c9-f4fc-08d8a02a7366', displayName: 'Inna'},
    { id: 'e223b1a2-3c05-4fbf-17cf-08d8a0c77c8f', displayName: 'Bezimienny'}
  ];

  get expense() : NewExpense {
    return this.form.value
  }

  form: FormGroup;

  constructor(
    private readonly fb: FormBuilder
  ) { }

  ngOnInit(): void {
    console.log(this.id);
  }

  onChangeWhoPaid(newValue: string) {
    this.form.get('whoPaidId').setValue(newValue);
  }
}
