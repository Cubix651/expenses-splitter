import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';

@Injectable()
export class ExpenseListService {

  constructor(
    private readonly http: HttpClient
  ) { }

  getExpenses(settlementId: string) {
    return this.http.get(`${environment.apiUrl}/settlements/${settlementId}/expenses`)
  }
}
