import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Expense, NewExpense } from '../models/expenses.model';

@Injectable()
export class ExpensesService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  getExpenses(settlementId: string): Observable<Expense[]> {
    return this.http.get<Expense[]>(`${this.apiUrl}/settlements/${settlementId}/expenses`);
  }

  addExpense(settlementId: string, expense: NewExpense): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/settlements/${settlementId}/expenses`, expense);
  }

  getExpense(settlementId: string, expenseId: string): Observable<Expense> {
    return this.http.get<Expense>(`${this.apiUrl}/settlements/${settlementId}/expenses/${expenseId}`);
  }

  updateExpense(settlementId: string, expenseId: string, expense: NewExpense): Observable<any> {
    return this.http.put<string>(`${this.apiUrl}/settlements/${settlementId}/expenses/${expenseId}`, expense);
  }

  deleteExpense(settlementId: string, expenseId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/settlements/${settlementId}/expenses/${expenseId}`);
  }
}
