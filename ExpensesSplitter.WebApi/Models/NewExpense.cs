using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    public class NewExpense
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount {get; set; }
        public Guid WhoPaidId { get; set; }
        public IReadOnlyList<Guid> Participants { get; set; }
    }
}