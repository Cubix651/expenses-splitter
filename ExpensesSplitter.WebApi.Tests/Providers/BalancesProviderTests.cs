using System;
using ExpensesSplitter.WebApi.Models;
using ExpensesSplitter.WebApi.Providers;
using ExpensesSplitter.WebApi.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace ExpensesSplitter.WebApi.Tests.Providers
{
    public class BalancesProviderTests
    {
        private readonly IExpensesRepository _expensesRepository = Substitute.For<IExpensesRepository>();
        private readonly ISettlementUsersRepository _settlementUsersRepository = Substitute.For<ISettlementUsersRepository>();
        private readonly ITransactionsRepository _transactionsRepository = Substitute.For<ITransactionsRepository>();
        private readonly ILogger<BalancesProvider> _logger = Substitute.For<ILogger<BalancesProvider>>();
        private readonly string _settlementId = "123";
        
        [Fact]
        public void GetBalances_SplitsExpensesBetweenAll()
        {
            var userA = CreateSettlementUser();
            var userB = CreateSettlementUser();
            var userC = CreateSettlementUser();
            var users = new[] {userA, userB, userC};
            _settlementUsersRepository.GetSettlementUsers(_settlementId)
                .Returns(users);
            var expenses = new[]
            {
                new Expense {WhoPaid = userA, Amount = 9}
            };
            _expensesRepository.GetExpenses(_settlementId)
                .Returns(expenses);
            var provider = CreateBalancesProvider();

            var balances = provider.GetBalances(_settlementId);
            
            Assert.Collection(balances,
                item => Assert.Equal(6M, item.Balance),
                item => Assert.Equal(-3M, item.Balance),
                item => Assert.Equal(-3M, item.Balance)
            );
        }
        
        [Fact]
        public void GetBalances_RoundsBalances()
        {
            var userA = CreateSettlementUser();
            var userB = CreateSettlementUser();
            var userC = CreateSettlementUser();
            var users = new[] {userA, userB, userC};
            _settlementUsersRepository.GetSettlementUsers(_settlementId)
                .Returns(users);
            var expenses = new[]
            {
                new Expense {WhoPaid = userA, Amount = 10}
            };
            _expensesRepository.GetExpenses(_settlementId)
                .Returns(expenses);
            var provider = CreateBalancesProvider();

            var balances = provider.GetBalances(_settlementId);
            
            Assert.Collection(balances,
                item => Assert.Equal(6.67M, item.Balance),
                item => Assert.Equal(-3.33M, item.Balance),
                item => Assert.Equal(-3.33M, item.Balance)
            );
        }
        
        [Fact]
        public void GetBalances_WhenTransactionsExist()
        {
            var userA = CreateSettlementUser();
            var userB = CreateSettlementUser();
            var userC = CreateSettlementUser();
            var users = new[] {userA, userB, userC};
            _settlementUsersRepository.GetSettlementUsers(_settlementId)
                .Returns(users);
            var expenses = new[]
            {
                new Expense {WhoPaid = userA, Amount = 9}
            };
            _expensesRepository.GetExpenses(_settlementId)
                .Returns(expenses);
            var transactions = new[]
            {
                new Transaction { From = userB, To = userA, Amount = 2},
                new Transaction { From = userC, To = userA, Amount = 1}
            };
            _transactionsRepository.GetTransactions(_settlementId)
                .Returns(transactions);
            var provider = CreateBalancesProvider();

            var balances = provider.GetBalances(_settlementId);
            
            Assert.Collection(balances,
                item => Assert.Equal(3M, item.Balance),
                item => Assert.Equal(-1M, item.Balance),
                item => Assert.Equal(-2M, item.Balance)
            );
        }
        
        [Fact]
        public void GetBalances_SplitsExpensesBetweenParticipants()
        {
            var userA = CreateSettlementUser();
            var userB = CreateSettlementUser();
            var userC = CreateSettlementUser();
            var users = new[] {userA, userB, userC};
            _settlementUsersRepository.GetSettlementUsers(_settlementId)
                .Returns(users);
            var expenses = new[]
            {
                new Expense
                {
                    WhoPaid = userA,
                    Amount = 10,
                    Participants = new []{userA, userB},
                }
            };
            _expensesRepository.GetExpenses(_settlementId)
                .Returns(expenses);
            var provider = CreateBalancesProvider();

            var balances = provider.GetBalances(_settlementId);
            
            Assert.Collection(balances,
                item => Assert.Equal(5M, item.Balance),
                item => Assert.Equal(-5M, item.Balance),
                item => Assert.Equal(0, item.Balance)
            );
        }
        
        [Fact]
        public void GetBalances_SplitsExpenses()
        {
            var userA = CreateSettlementUser();
            var userB = CreateSettlementUser();
            var userC = CreateSettlementUser();
            var users = new[] {userA, userB, userC};
            _settlementUsersRepository.GetSettlementUsers(_settlementId)
                .Returns(users);
            var expenses = new[]
            {
                new Expense {WhoPaid = userA, Amount = 9},
                new Expense {WhoPaid = userA, Amount = 10, Participants = new []{userA, userB}},
                new Expense {WhoPaid = userB, Amount = 8, Participants = new []{userB, userC}},
            };
            _expensesRepository.GetExpenses(_settlementId)
                .Returns(expenses);
            var provider = CreateBalancesProvider();

            var balances = provider.GetBalances(_settlementId);
            
            Assert.Collection(balances,
                item => Assert.Equal(11M, item.Balance),
                item => Assert.Equal(-4M, item.Balance),
                item => Assert.Equal(-7M, item.Balance)
            );
        }

        BalancesProvider CreateBalancesProvider()
        {
            return new BalancesProvider(
                _expensesRepository,
                _settlementUsersRepository,
                _logger,
                _transactionsRepository
            );
        }

        SettlementUser CreateSettlementUser()
        {
            return new SettlementUser
            {
                Id = Guid.NewGuid()
            };
        }
    }
}