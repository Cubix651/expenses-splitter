import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NewSettlementComponent } from './new-settlement/new-settlement.component';
import { SettlementListComponent } from './settlement-list/settlement-list.component';
import { SettlementSummaryComponent } from './settlement-summary/settlement-summary.component';
import { Login } from './login/login.component';
import { Register } from './register/register.component';
import { ExpenseListComponent } from './expense-list/expense-list.component';
import { ExpenseEditorComponent } from './expense-editor/expense-editor.component';
import { NewExpenseComponent } from './new-expense/new-expense.component';
import { ExpenseDetailsComponent } from './expense-details/expense-details.component';
import { SettlementUserEditorComponent } from './settlementuser-editor/settlementuser-editor.component';
import { BalancesComponent } from './balances/balances.component';

@NgModule({
  declarations: [
    AppComponent,
    NewSettlementComponent,
    SettlementListComponent,
    SettlementSummaryComponent,
    Login,
    Register,
    ExpenseListComponent,
    ExpenseEditorComponent,
    NewExpenseComponent,
    ExpenseDetailsComponent,
    SettlementUserEditorComponent,
    BalancesComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
