using System;
using System.Collections.Generic;
using System.Linq;
using ExpensesSplitter.WebApi.Models;

namespace ExpensesSplitter.WebApi.Algorithms
{
    public class SettlementSolver
    {
        private readonly IList<UserBalance> _balances;
        private List<UserBalance> _positive;
        private List<UserBalance> _negative;
        private List<SolutionTransaction> _solutionTransactions;
        private bool _reversedMode;

        public SettlementSolver(IEnumerable<UserBalance> balances)
        {
            _balances = balances.ToList();
        }

        public ICollection<SolutionTransaction> Solve()
        {
            GroupBalances();
            _solutionTransactions = new List<SolutionTransaction>();
            _reversedMode = false;

            while (_positive.Count > 0 && _negative.Count > 0)
            {
                if (_positive.Last().Balance < _negative.Last().Balance)
                    ReverseMode();
                
                var value = _negative.Last().Balance;
                var from = _negative.Last().User;
                var firstGreaterOrEqualPositiveBalance = _positive.First(ub => ub.Balance >= value);
                var to = firstGreaterOrEqualPositiveBalance.User;
                
                AddTransaction(value, from, to);
                
                _positive.RemoveAll(ub => ub.User.Id == firstGreaterOrEqualPositiveBalance.User.Id);
                _negative.RemoveAt(_negative.Count - 1);
                _positive.Add(new UserBalance
                {
                    User = to,
                    Balance = firstGreaterOrEqualPositiveBalance.Balance - value
                });
                _positive = _positive
                    .OrderBy(ub => ub.Balance)
                    .ToList();
            }

            return _solutionTransactions;
        }

        private void GroupBalances()
        {
            _positive = _balances
                .Where(ub => ub.Balance > 0)
                .OrderBy(ub => ub.Balance)
                .ToList();
            _negative = _balances
                .Where(ub => ub.Balance < 0)
                .Select(ub => -ub)
                .OrderBy(ub => ub.Balance)
                .ToList();
        }

        private void ReverseMode()
        {
            (_negative, _positive) = (_positive, _negative);
            _reversedMode = !_reversedMode;
        }

        private void AddTransaction(decimal amount, SettlementUser from, SettlementUser to)
        {
            _solutionTransactions.Add(new SolutionTransaction
            {
                Amount = amount,
                From = _reversedMode ? to : from,
                To = _reversedMode ? from : to
            });
        }
    }
}