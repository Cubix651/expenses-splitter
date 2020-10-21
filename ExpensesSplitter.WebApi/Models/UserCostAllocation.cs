using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    public partial class UserCostAllocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User user { get; set; }
        public Role role { get; set; }
        public ExpenseGroup expenseGroup { get; set; }
    }
}
