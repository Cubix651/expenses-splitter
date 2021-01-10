using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesSplitter.WebApi.Database.Models
{
    public class ExpenseParticipation
    {
        public Guid ExpenseId { get; set; }
        public virtual Expense Expense { get; set; }
        public Guid SettlementUserId { get; set; }
        public virtual SettlementUser SettlementUser { get; set; }
    }
}