import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SolutionTransaction } from '../models/solution-transaction.model';
import { SettlementSolutionService } from '../services/settlement-solution.service';

@Component({
  selector: 'app-settlement-solution',
  templateUrl: './settlement-solution.component.html',
  styleUrls: ['./settlement-solution.component.scss'],
  providers: [ SettlementSolutionService ]
})
export class SettlementSolutionComponent implements OnInit {
  settlementId: string;
  loadErrorOccurred: boolean = false;
  solutionTransactions: SolutionTransaction[];

  constructor(
    private readonly service: SettlementSolutionService,
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
}
