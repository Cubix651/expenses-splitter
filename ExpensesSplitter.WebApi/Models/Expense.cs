using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    public partial class Expense
    {
        public int Id { get; set; }
        public UserCostAllocation userCostAllocation { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }


    }
}