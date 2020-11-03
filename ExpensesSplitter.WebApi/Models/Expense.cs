using System;
using System.Collections.Generic;

namespace ExpensesSplitter.WebApi.Models
{   
    /*
      Expense to wydatki przydzielone poszczególnemu użytkownikowi rozliczenia. W rozliczeniu to będą wiersze do których się wpisuje wydatki
     */
    public partial class Expense
    {
        public int Id { get; set; }
        public UserCostAllocation userCostAllocation { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }


    }
}
