using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using ExpensesSplitter.WebApi.Models;
using ExpensesSplitter.WebApi.Repositories;
using Microsoft.Extensions.Logging;

namespace ExpensesSplitter.WebApi.Providers
{
    public interface IBalancesProvider
    {
        IReadOnlyList<UserBalance> GetBalances(string settlementId);
    }
    
    public class BalancesProvider : IBalancesProvider
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly ISettlementUsersRepository _settlementUsersRepository;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly ILogger<BalancesProvider> _logger;

        public BalancesProvider(IExpensesRepository expensesRepository,
            ISettlementUsersRepository settlementUsersRepository,
            ILogger<BalancesProvider> logger,
            ITransactionsRepository transactionsRepository)
        {
            _expensesRepository = expensesRepository;
            _settlementUsersRepository = settlementUsersRepository;
            _logger = logger;
            _transactionsRepository = transactionsRepository;
        }

        public IReadOnlyList<UserBalance> GetBalances(string settlementId)
        {
            var expenses = _expensesRepository.GetExpenses(settlementId).ToList();
            var expensesSumByWhoPaid = expenses
                .GroupBy(e => e.WhoPaid.Id)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
            var settlementUsers = _settlementUsersRepository.GetSettlementUsers(settlementId)
                .ToList();
            var usersCount = settlementUsers.Count();
            var expensesSumByParticipant = settlementUsers
                .ToDictionary(u => u.Id, u => 
                    Round(expenses
                        .Where(e => e.Participants.IsNullOrEmpty() || e.Participants.Any(p => p.Id == u.Id))
                        .Sum(e => e.Amount / (e.Participants.IsNullOrEmpty() ? usersCount : e.Participants.Count))));
            var transactions = _transactionsRepository.GetTransactions(settlementId)
                .ToList();
            var outgoingTransactionsSumByUser = transactions
                .GroupBy(t => t.From.Id)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
            var incomingTransactionsSumByUser = transactions
                .GroupBy(t => t.To.Id)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
            return settlementUsers
                .Select(u => new UserBalance
                {
                    User = u,
                    Balance = expensesSumByWhoPaid.GetValueOrDefault(u.Id, 0)
                              - expensesSumByParticipant.GetValueOrDefault(u.Id, 0)
                              + outgoingTransactionsSumByUser.GetValueOrDefault(u.Id, 0)
                              - incomingTransactionsSumByUser.GetValueOrDefault(u.Id, 0)
                })
                .ToList();
        }

        private static decimal Round(decimal value)
        {
            return Math.Floor(value * 100) / 100;
        }
    }
}