import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NewSettlementComponent } from './new-settlement/new-settlement.component';
import { SettlementListComponent } from './settlement-list/settlement-list.component';
import { SettlementSummaryComponent } from './settlement-summary/settlement-summary.component';


const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'settlements' },
  {
    path: 'settlements', children: [
      { path: '', pathMatch: 'full', component: SettlementListComponent },
      { path: 'create', component: NewSettlementComponent },
      { path: ':id', component: SettlementSummaryComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
