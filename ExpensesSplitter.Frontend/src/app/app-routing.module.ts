import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NewSettlementComponent } from './new-settlement/new-settlement.component';
import { SettlementListComponent } from './settlement-list/settlement-list.component';
import { SettlementSummaryComponent } from './settlement-summary/settlement-summary.component';
import { ExpenseListComponent } from './expense-list/expense-list.component';
import { Login } from './login/login.component';
import { Register } from './register/register.component';
import { NewExpenseComponent } from './new-expense/new-expense.component';
import { ExpenseDetailsComponent } from './expense-details/expense-details.component';
import { BalancesComponent } from './balances/balances.component';


const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'settlements' },
  {
    path: 'settlements', children: [
      { path: '', pathMatch: 'full', component: SettlementListComponent },
      { path: 'create', component: NewSettlementComponent },
      { path: ':settlementId', children: [
        { path: '', pathMatch: 'full', component: SettlementSummaryComponent},
        { path: 'expenses', children: [
          { path: '', pathMatch: 'full', component: ExpenseListComponent },
          { path: 'create', component: NewExpenseComponent},
          { path: ':expenseId', component: ExpenseDetailsComponent}
        ]},
        { path: 'balances', component: BalancesComponent },
      ] },
    ]
  },
  {path:'login', children:[
    {path:'', pathMatch:'full', component: Login}
  ]},
  {path:'register', children:[
    {path:'', pathMatch:'full', component: Register}
  ]},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
