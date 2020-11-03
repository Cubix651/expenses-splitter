using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    /* Cost Allocation to rozliczenie które będzie miało nazwę, użytkownika który je stworzył oraz listę użytkowników(UserCostAllocation) którzy biorą w nim udział. 
     */
    public partial class CostAllocation
    {
        public CostAllocation()
        {
            UserCostAllocation = new HashSet<UserCostAllocation>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public User user { get; set; }
        public ICollection<UserCostAllocation> UserCostAllocation { get; set; }
        
    }
}
