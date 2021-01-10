using System;

namespace ExpensesSplitter.WebApi.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public SettlementUser From { get; set; }
        public SettlementUser To { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
    }
}