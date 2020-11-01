using ExpensesSplitter.WebApi.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpensesSplitter.WebApi.Infrastructure
{
    public static class DatabaseContexts
    {
        public static void AddDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExpensesSplitterContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ExpensesSplitter")));
        }
    }
}