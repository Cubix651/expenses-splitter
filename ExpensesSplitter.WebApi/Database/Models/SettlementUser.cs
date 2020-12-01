using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesSplitter.WebApi.Database.Models
{
    public class SettlementUser
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SettlementId { get; set; }

    }
}
