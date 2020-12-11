using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesSplitter.WebApi.Database.Models
{
    public class SettlementUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string SettlementId { get; set; }
        public string DisplayName { get; set; }
    }
}
