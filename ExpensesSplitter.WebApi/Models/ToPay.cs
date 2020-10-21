using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    public partial class ToPay
    {
        public int Id { get; set; }
        public User user { get; set; }
        public float Amount { get; set; }
        public bool payed { get; set; }
        public UserCostAllocation userCostAllocation { get; set; }

    }
}
