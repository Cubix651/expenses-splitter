import { SettlementUser } from './settlement-user.model';

export interface NewExpense {
    name: string;
    description: string;
    dateTime: Date;
    amount: number;
    whoPaidId: string;
    participants: string[];
}

export interface Expense {
    id: string;
    name: string;
    description: string;
    dateTime: Date;
    amount: number;
    whoPaid: SettlementUser;
    participants: SettlementUser[];
}
