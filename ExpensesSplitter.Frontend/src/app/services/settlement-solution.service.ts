import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SolutionTransaction } from '../models/solution-transaction.model';

@Injectable()
export class SettlementSolutionService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  getSolutionTransactions(settlementId: string): Observable<SolutionTransaction[]> {
    return this.http.get<SolutionTransaction[]>(`${this.apiUrl}/settlements/${settlementId}/solution`);
  }
}
