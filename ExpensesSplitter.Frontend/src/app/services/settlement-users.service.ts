import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SettlementUser, NewSettlementUser } from '../models/settlement-user.model';

@Injectable()
export class SettlementUsersService {
  private readonly apiUrl = environment.apiUrl;

  constructor(
    private readonly http: HttpClient
  ) { }

  getUsers(settlementId: string): Observable<SettlementUser[]> {
    return this.http.get<SettlementUser[]>(`${this.apiUrl}/settlements/${settlementId}/users`);
  }
  addUser(settlementId: string, settlementUser: NewSettlementUser): Observable<SettlementUser[]> {
    return this.http.post<SettlementUser[]>(`${this.apiUrl}/settlements/${settlementId}/users`, settlementUser);
  }
}
