using ExpensesSplitter.WebApi.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesSplitter.WebApi.Database
{
    public class ExpensesSplitterContext : DbContext
    {
        public ExpensesSplitterContext(DbContextOptions<ExpensesSplitterContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseParticipation>()
                .HasKey(ep => new {ep.ExpenseId, ep.SettlementUserId});
        }

        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SettlementUser> SettlementUsers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Expense> Expenses {get; set;}
        public DbSet<ExpenseParticipation> ExpenseParticipations { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}