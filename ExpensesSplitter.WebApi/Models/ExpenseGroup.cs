using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    public partial class ExpenseGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Family { get; set; }
        
    }
}