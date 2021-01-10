using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesSplitter.WebApi.Database.Models
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public virtual SettlementUser From { get; set; }
        public Guid FromId { get; set; }
        public virtual SettlementUser To { get; set; }
        public Guid ToId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string SettlementId {get; set;}
        public virtual Settlement Settlement { get; set; }
    }
}