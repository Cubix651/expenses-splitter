import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NewTransaction, Transaction } from '../models/transaction.model';

@Injectable()
export class TransactionsService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  getTransactions(settlementId: string): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.apiUrl}/settlements/${settlementId}/transactions`);
  }

  addTransaction(settlementId: string, transaction: NewTransaction): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/settlements/${settlementId}/transactions`, transaction);
  }

  getTransaction(settlementId: string, transactionId: string): Observable<Transaction> {
    return this.http.get<Transaction>(`${this.apiUrl}/settlements/${settlementId}/transactions/${transactionId}`);
  }

  updateTransaction(settlementId: string, transactionId: string, transaction: NewTransaction): Observable<any> {
    return this.http.put<string>(`${this.apiUrl}/settlements/${settlementId}/transactions/${transactionId}`, transaction);
  }

  deleteTransaction(settlementId: string, transactionId: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/settlements/${settlementId}/transactions/${transactionId}`);
  }
}
