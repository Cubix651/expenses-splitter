export interface NewExpense {
    name: string;
    description: string;
    amount: number;
}

export interface Expense extends NewExpense {
    id: string;
}
