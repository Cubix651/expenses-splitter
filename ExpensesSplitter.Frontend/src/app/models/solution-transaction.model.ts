import { SettlementUser } from './settlement-user.model';

export interface SolutionTransaction {
  from: SettlementUser
  to: SettlementUser
  amount: number;
}
