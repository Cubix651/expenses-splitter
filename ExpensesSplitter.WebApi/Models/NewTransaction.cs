using System;

namespace ExpensesSplitter.WebApi.Models
{
    public class NewTransaction
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public decimal Amount { get; set; }
    }
}