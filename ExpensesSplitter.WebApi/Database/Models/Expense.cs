using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesSplitter.WebApi.Database.Models
{
    public class Expense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount {get; set; }
        public string SettlementId {get; set;}
        public virtual Settlement Settlement { get; set; }
        public Guid WhoPaidId { get; set;}
        public virtual SettlementUser WhoPaid { get; set;}
    }
}