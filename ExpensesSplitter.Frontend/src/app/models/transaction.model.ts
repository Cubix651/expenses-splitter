import { SettlementUser } from './settlement-user.model';

export interface NewTransaction {
  fromId: string;
  toId: string;
  dateTime: Date;
  amount: number;
}

export interface Transaction {
  id: string;
  from: SettlementUser;
  to: SettlementUser;
  dateTime: Date;
  amount: number;
}
