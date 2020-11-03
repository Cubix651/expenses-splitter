using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    /*
     ExpenseGroup to grupa rozliczeniowa, tutaj jeszcze trzeba sie zastanowić jak będzie to konkretnie działać
     */
    public partial class ExpenseGroup
    {
        public int Id { get; set; }
        public CostAllocation cost { get; set; }
        public string Name { get; set; }
        public bool Family { get; set; }
        
    }
}
