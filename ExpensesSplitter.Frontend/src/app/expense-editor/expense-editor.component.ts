import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NewExpense } from '../models/expenses.model';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { SettlementUser } from '../models/settlement-user.model';
import { SettlementUsersService } from '../services/settlement-users.service';

@Component({
  selector: 'app-expense-editor',
  templateUrl: './expense-editor.component.html',
  styleUrls: ['./expense-editor.component.scss'],
  providers: [ SettlementUsersService ]
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
  settlementId: string;

  people: SettlementUser[] = [];

  get expense() : NewExpense {
    return this.form.value
  }

  form: FormGroup;

  constructor(
    private readonly fb: FormBuilder,
    private readonly settlementUsersService: SettlementUsersService,
  ) { }

  ngOnInit(): void {
    this.fetchSettlementUsers();
  }

  onChangeWhoPaid(newValue: string) {
    this.form.get('whoPaidId').setValue(newValue);
  }

  private fetchSettlementUsers() {
    this.settlementUsersService.getUsers(this.settlementId)
      .subscribe(users => {
        this.people = users;
        if (this.form.get('whoPaidId').value == null) {
          this.onChangeWhoPaid(users[0].id);
        }
      });
  }
}
