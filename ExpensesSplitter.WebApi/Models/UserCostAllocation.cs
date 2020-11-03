using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{
    /*
     To jest kazdy dodany użytkownik do rozliczenia. W rozliczeniu to będzie podstawa każdej kolumny. 
     */
    public partial class UserCostAllocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User user { get; set; }
        public Role role { get; set; }
        public ExpenseGroup expenseGroup { get; set; }
    }
}
