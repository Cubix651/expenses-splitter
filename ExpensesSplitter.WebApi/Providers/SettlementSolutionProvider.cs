using System.Collections.Generic;
using ExpensesSplitter.WebApi.Algorithms;
using ExpensesSplitter.WebApi.Models;
using Microsoft.Extensions.Logging;

namespace ExpensesSplitter.WebApi.Providers
{
    public interface ISettlementSolutionProvider
    {
        ICollection<SolutionTransaction> GetSolutionTransactions(string settlementId);
    }
    
    public class SettlementSolutionProvider : ISettlementSolutionProvider
    {
        private readonly IBalancesProvider _balancesProvider;
        private readonly ILogger<SettlementSolutionProvider> _logger;

        public SettlementSolutionProvider(IBalancesProvider balancesProvider,
            ILogger<SettlementSolutionProvider> logger)
        {
            _balancesProvider = balancesProvider;
            _logger = logger;
        }

        public ICollection<SolutionTransaction> GetSolutionTransactions(string settlementId)
        {
            var balances = _balancesProvider.GetBalances(settlementId);
            var solver = new SettlementSolver(balances);
            return solver.Solve();
        }
    }
}