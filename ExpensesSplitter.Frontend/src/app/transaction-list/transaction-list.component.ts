import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Transaction } from '../models/transaction.model';
import { TransactionsService } from '../services/transactions.service';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss'],
  providers: [ TransactionsService ]
})
export class TransactionListComponent implements OnInit {
  settlementId: string;
  loadErrorOccurred: boolean = false;
  deleteErrorOccurred: boolean = false;
  transactions: Transaction[];

  constructor(
    private readonly service: TransactionsService,
    route: ActivatedRoute,
  ) {
    this.settlementId = route.snapshot.params.settlementId;
  }


  ngOnInit(): void {
    this.fetchTransactions();
  }

  private fetchTransactions() {
    this.service.getTransactions(this.settlementId)
      .subscribe({
        next: transactions => this.transactions = transactions,
        error: error => {
          console.error('Error during fetching transactions', error);
          this.loadErrorOccurred = true;
        }
      });
  }

  deleteTransaction(transactionId: string) {
    this.deleteErrorOccurred = false;

    this.service.deleteTransaction(this.settlementId, transactionId)
      .subscribe({
        complete: () => this.fetchTransactions(),
        error: error => {
          console.error('Error during removing transaction', error);
          this.deleteErrorOccurred = true;
        }
      });
  }

  trackById(index: number, transaction: Transaction) {
    return transaction.id;
  }
}
