import { SettlementUser } from './settlement-user.model';

export interface NewExpense {
    name: string;
    description: string;
    amount: number;
    whoPaidId: string;
}

export interface Expense {
    id: string;
    name: string;
    description: string;
    amount: number;
    whoPaid: SettlementUser;
}
