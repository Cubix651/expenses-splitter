using System;
using ExpensesSplitter.WebApi.Algorithms;
using ExpensesSplitter.WebApi.Models;
using Xunit;

namespace ExpensesSplitter.WebApi.Tests.Algorithms
{
    public class SettlementSolverTests
    {
        [Fact]
        public void Solve_SimpleScenario()
        {
            var balances = new[]
            {
                CreateUserBalance("A", 100),
                CreateUserBalance("B", 90),
                CreateUserBalance("C", -50),
                CreateUserBalance("D", -50),
                CreateUserBalance("E", -90),
            };
            var solver = new SettlementSolver(balances);

            var result = solver.Solve();
            
            Assert.Equal(3, result.Count);
            Assert.Contains(result, t => IsTransaction(t, "C", "A", 50));
            Assert.Contains(result, t => IsTransaction(t, "D", "A", 50));
            Assert.Contains(result, t => IsTransaction(t, "E", "B", 90));
        }
        
        [Fact]
        public void Solve_QuiteComplexScenario()
        {
            var balances = new[]
            {
                CreateUserBalance("A", 100),
                CreateUserBalance("B", 91),
                CreateUserBalance("C", -10),
                CreateUserBalance("D", -11),
                CreateUserBalance("E", -40),
                CreateUserBalance("F", -40),
                CreateUserBalance("G", -90),
            };
            var solver = new SettlementSolver(balances);

            var result = solver.Solve();
            
            Assert.Equal(6, result.Count);
            Assert.Contains(result, t => IsTransaction(t, "G", "B", 90));
            Assert.Contains(result, t => IsTransaction(t, "F", "A", 40));
            Assert.Contains(result, t => IsTransaction(t, "E", "A", 40));
            Assert.Contains(result, t => IsTransaction(t, "D", "A", 11));
            Assert.Contains(result, t => IsTransaction(t, "C", "A", 9));
            Assert.Contains(result, t => IsTransaction(t, "C", "B", 1));
        }
        
        [Fact]
        public void Solve_OnePersonReturnsMoneyToEverybodyElse()
        {
            var balances = new[]
            {
                CreateUserBalance("A", 1),
                CreateUserBalance("B", 2),
                CreateUserBalance("C", 3),
                CreateUserBalance("D", 4),
                CreateUserBalance("E", -10),
            };
            var solver = new SettlementSolver(balances);

            var result = solver.Solve();
            
            Assert.Equal(4, result.Count);
            Assert.Contains(result, t => IsTransaction(t, "E", "A", 1));
            Assert.Contains(result, t => IsTransaction(t, "E", "B", 2));
            Assert.Contains(result, t => IsTransaction(t, "E", "C", 3));
            Assert.Contains(result, t => IsTransaction(t, "E", "D", 4));
        }
        
        [Fact]
        public void Solve_WhenBalancesAreZero_ReturnsEmptyList()
        {
            var balances = new[]
            {
                CreateUserBalance("A", 0),
                CreateUserBalance("B", 0),
                CreateUserBalance("C", 0),
                CreateUserBalance("D", 0),
                CreateUserBalance("E", 0),
            };
            var solver = new SettlementSolver(balances);

            var result = solver.Solve();
            
            Assert.Empty(result);
        }
        
        [Fact]
        public void Solve_EverybodyReturnsMoneyToOnePerson()
        {
            var balances = new[]
            {
                CreateUserBalance("A", -1),
                CreateUserBalance("B", -2),
                CreateUserBalance("C", -3),
                CreateUserBalance("D", -4),
                CreateUserBalance("E", 10),
            };
            var solver = new SettlementSolver(balances);

            var result = solver.Solve();
            
            Assert.Equal(4, result.Count);
            Assert.Contains(result, t => IsTransaction(t, "A", "E", 1));
            Assert.Contains(result, t => IsTransaction(t, "B", "E", 2));
            Assert.Contains(result, t => IsTransaction(t, "C", "E", 3));
            Assert.Contains(result, t => IsTransaction(t, "D", "E", 4));
        }
        
        [Fact]
        public void Solve_WhenSumOfBalancesIsNotZero()
        {
            var balances = new[]
            {
                CreateUserBalance("A", 6.67M),
                CreateUserBalance("B", -3.33M),
                CreateUserBalance("C", -3.33M),
            };
            var solver = new SettlementSolver(balances);

            var result = solver.Solve();
            
            Assert.Equal(2, result.Count);
            Assert.Contains(result, t => IsTransaction(t, "B", "A", 3.33M));
            Assert.Contains(result, t => IsTransaction(t, "C", "A", 3.33M));
        }

        bool IsTransaction(SolutionTransaction t, string from, string to, decimal amount)
        {
            return t.From.DisplayName == from && t.To.DisplayName == to && t.Amount == amount;
        }

        UserBalance CreateUserBalance(string name, decimal balance)
        {
            return new UserBalance
            {
                User = new SettlementUser
                {
                    DisplayName = name,
                    Id = Guid.NewGuid()
                },
                Balance = balance
            };
        }
    }
}
