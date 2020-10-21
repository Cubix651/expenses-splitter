using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    public partial class CostAllocation
    {
        public CostAllocation()
        {
            UserCostAllocation = new HashSet<UserCostAllocation>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public User user { get; set; }
        public ICollection<UserCostAllocation> user { get; set; }
        
    }
}
