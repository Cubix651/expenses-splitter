import { SettlementUser } from './settlement-user.model';

export interface NewTransaction {
  fromId: string;
  toId: string;
  amount: number;
}

export interface Transaction {
  id: string;
  from: SettlementUser;
  to: SettlementUser;
  amount: number;
}
