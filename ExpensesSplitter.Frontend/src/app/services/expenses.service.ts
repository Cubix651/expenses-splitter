import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Expense } from '../expense-list/expense.model';

@Injectable()
export class ExpensesService {

  constructor(
    private readonly http: HttpClient
  ) { }

  getExpenses(settlementId: string): Observable<Expense[]> {
    return this.http.get<Expense[]>(`${environment.apiUrl}/settlements/${settlementId}/expenses`);
  }
}
