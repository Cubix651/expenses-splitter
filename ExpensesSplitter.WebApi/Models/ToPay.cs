using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    /*
     Pod rozliczeniem będzie pokazane ile dana osoba ma zapłacić i komu
     */
    public partial class ToPay
    {
        public int Id { get; set; }
        public User user { get; set; }
        public float Amount { get; set; }
        public bool payed { get; set; }
        public UserCostAllocation userCostAllocation { get; set; }

    }
}
