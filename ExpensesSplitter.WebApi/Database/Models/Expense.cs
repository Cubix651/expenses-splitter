using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesSplitter.WebApi.Database.Models
{
    public class Expense
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount {get; set; }
        public virtual Settlement Settlement { get; set; }
    }
}