import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SolutionTransaction } from '../models/solution-transaction.model';
import { SettlementSolutionService } from '../services/settlement-solution.service';
import { TransactionsService } from '../services/transactions.service';

@Component({
  selector: 'app-settlement-solution',
  templateUrl: './settlement-solution.component.html',
  styleUrls: ['./settlement-solution.component.scss'],
  providers: [
    SettlementSolutionService,
    TransactionsService,
  ]
})
export class SettlementSolutionComponent implements OnInit {
  settlementId: string;
  loadErrorOccurred: boolean = false;
  markAsPaidErrorOccurred: boolean = false;
  solutionTransactions: SolutionTransaction[];

  constructor(
    private readonly service: SettlementSolutionService,
    private readonly transactionsService: TransactionsService,
    route: ActivatedRoute,
  ) {
    this.settlementId = route.snapshot.params.settlementId;
  }


  ngOnInit(): void {
    this.fetchSolutionTransactions();
  }

  private fetchSolutionTransactions() {
    this.service.getSolutionTransactions(this.settlementId)
      .subscribe({
        next: transactions => this.solutionTransactions = transactions,
        error: error => {
          console.error('Error during fetching solution transactions', error);
          this.loadErrorOccurred = true;
        }
      });
  }

  trackById(index: number, solutionTransaction: SolutionTransaction) {
    return solutionTransaction.from.id + solutionTransaction.to.id;
  }

  markAsPaid(solutionTransaction: SolutionTransaction) {
    this.markAsPaidErrorOccurred = false;
    const now = new Date();
    const nowUtc = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate(), now.getHours(), now.getMinutes()));
    this.transactionsService.addTransaction(this.settlementId, {
      fromId: solutionTransaction.from.id,
      toId: solutionTransaction.to.id,
      dateTime: nowUtc,
      amount: solutionTransaction.amount
    })
      .subscribe({
        next: _ => this.fetchSolutionTransactions(),
        error: error => {
          console.error('Error during creating transaction', error);
          this.markAsPaidErrorOccurred = true;
        }
      })
  }
}
