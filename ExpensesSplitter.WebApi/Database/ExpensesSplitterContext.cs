using ExpensesSplitter.WebApi.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesSplitter.WebApi.Database
{
    public class ExpensesSplitterContext : DbContext
    {
        public ExpensesSplitterContext(DbContextOptions<ExpensesSplitterContext> options)
            : base(options)
        { }

        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SettlementUser> SettlementUsers { get; set; }
        public DbSet<Expense> Expenses {get; set;}
    }
}