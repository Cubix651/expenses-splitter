using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    public class Expense
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount {get; set; }
        public SettlementUser WhoPaid {get; set;}
        public IReadOnlyList<SettlementUser> Participants { get; set; }
    }
}