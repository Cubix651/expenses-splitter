using System;

namespace ExpensesSplitter.WebApi.Models
{
    public class Expense : NewExpense
    {
        public Guid Id { get; set; }
    }
}