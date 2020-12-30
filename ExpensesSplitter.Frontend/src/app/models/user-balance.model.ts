import { SettlementUser } from './settlement-user.model';

export interface UserBalance {
  user: SettlementUser
  balance: number;
}
