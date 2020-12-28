using ExpensesSplitter.WebApi.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensesSplitter.WebApi.Infrastructure
{
    public static class Registry
    {
        public static IServiceCollection AddExpensesSplitter(this IServiceCollection services)
        {
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
            services.AddScoped<ISettlementUsersRepository, SettlementUsersRepository>();
            return services;
        }
    }
}
