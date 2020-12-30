import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserBalance } from '../models/user-balance.model';

@Injectable()
export class BalancesService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  getBalances(settlementId: string): Observable<UserBalance[]> {
    return this.http.get<UserBalance[]>(`${this.apiUrl}/settlements/${settlementId}/balances`);
  }
}
