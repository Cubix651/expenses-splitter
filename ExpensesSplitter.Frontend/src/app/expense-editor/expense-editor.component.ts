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
    const splitBetweenAll = value.participants === null || value.participants.length == 0;
    this.form = this.fb.group({
      name: [value.name],
      description: [value.description],
      amount: [value.amount],
      whoPaidId: [value.whoPaidId],
      splitBetweenAll: [splitBetweenAll],
      participants: this.fb.group({}),
    })
    this.participants = this.form.get('participants') as FormGroup;

    if (value.participants != null) {
      value.participants.forEach(p =>
        this.participants.addControl(p, this.fb.control(true)));
    }
    this.updateParticipansDisability(splitBetweenAll);
    this.form.get('splitBetweenAll').valueChanges.subscribe({
      next: value => this.updateParticipansDisability(value)
    });
  }

  @Input()
  settlementId: string;

  people: SettlementUser[] = [];

  get expense() : NewExpense {
    let participants: string[] = [];
    if (!this.form.get('splitBetweenAll').value) {
      participants = this.people
        .filter(p => this.participants.get(p.id).value)
        .map(p => p.id);
    }
    return {
      ...this.form.value,
      participants: participants
    };
  }

  form: FormGroup;
  private participants: FormGroup;

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
        users.forEach(u => {
          if (!this.participants.contains(u.id)) {
            this.participants.addControl(u.id, this.fb.control(false));
          }
        });
        this.updateParticipansDisability(this.form.get('splitBetweenAll').value);
        this.people = users;
        if (this.form.get('whoPaidId').value == null) {
          this.onChangeWhoPaid(users[0].id);
        }
      });
  }

  private updateParticipansDisability(splitBetweenAll: boolean) {
    if (splitBetweenAll) {
      this.participants.disable();
    } else {
      this.participants.enable();
    }
  }
}
