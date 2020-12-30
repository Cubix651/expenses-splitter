import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserBalance } from '../models/user-balance.model';
import { BalancesService } from '../services/balances.service';

@Component({
  selector: 'app-balances',
  templateUrl: './balances.component.html',
  styleUrls: ['./balances.component.scss'],
  providers: [ BalancesService ]
})
export class BalancesComponent implements OnInit {
  settlementId: string;
  loadErrorOccurred: boolean = false;
  balances: UserBalance[];

  constructor(
    private readonly service: BalancesService,
    route: ActivatedRoute,
  ) {
    this.settlementId = route.snapshot.params.settlementId;
  }


  ngOnInit(): void {
    this.fetchBalances();
  }

  private fetchBalances() {
    this.service.getBalances(this.settlementId)
      .subscribe({
        next: balances => this.balances = balances,
        error: error => {
          console.error('Error during fetching balances', error);
          this.loadErrorOccurred = true;
        }
      });
  }

  trackById(index: number, userBalance: UserBalance) {
    return userBalance.user.id;
  }
}
