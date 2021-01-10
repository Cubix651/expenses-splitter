using System;
using System.Collections.Generic;
using System.Linq;
using ExpensesSplitter.WebApi.Database;
using ExpensesSplitter.WebApi.Models;
using AutoMapper;
using ExpensesSplitter.WebApi.Database.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Expense = ExpensesSplitter.WebApi.Models.Expense;

namespace ExpensesSplitter.WebApi.Repositories
{
    public interface IExpensesRepository
    {
        IEnumerable<Expense> GetExpenses(string settlementId);
        Expense GetExpense(string settlementId, Guid expenseId);
        Guid CreateExpense(string settlementId, NewExpense expense);
        void DeleteExpense(string settlementId, Guid expenseId);
        void UpdateExpense(string settlementId, Guid expenseId, NewExpense expense);
    }

    public class ExpensesRepository : IExpensesRepository
    {
        private readonly ExpensesSplitterContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ExpensesRepository> _logger;

        public ExpensesRepository(ExpensesSplitterContext context,
            IMapper mapper,
            ILogger<ExpensesRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<Expense> GetExpenses(string settlementId)
        {
            var entities = _context.Expenses
                .Where(e => e.SettlementId == settlementId);
            return _mapper.ProjectTo<Expense>(entities);
        }

        public Expense GetExpense(string settlementId, Guid expenseId)
        {
            var entity = _context.Expenses
                .Where(e => e.SettlementId == settlementId)
                .First(e => e.Id == expenseId);
            return _mapper.Map<Expense>(entity);
        }

        public Guid CreateExpense(string settlementId, NewExpense expense)
        {
            var expenseEntity = _mapper.Map<Database.Models.Expense>(expense);
            expenseEntity.SettlementId = settlementId;
            _context.Expenses.Add(expenseEntity);
            _context.SaveChanges();
            _context.ExpenseParticipations.AddRange(
                expense.Participants.Select(userId => new ExpenseParticipation
                {
                    ExpenseId = expenseEntity.Id,
                    SettlementUserId = userId
                }));
            _context.SaveChanges();
            return expenseEntity.Id;
        }

        public void DeleteExpense(string settlementId, Guid expenseId)
        {
            var expenseEntity = _context.Expenses
                .Where(e => e.SettlementId == settlementId)
                .First(e => e.Id == expenseId);
            _context.Expenses.Remove(expenseEntity);
            var expenseParticipationEntities = _context.ExpenseParticipations
                .Where(p => p.ExpenseId == expenseId);
            _context.ExpenseParticipations.RemoveRange(expenseParticipationEntities);
            _context.SaveChanges();
        }

        public void UpdateExpense(string settlementId, Guid expenseId, NewExpense expense)
        {
            var entity = _mapper.Map<Database.Models.Expense>(expense);
            entity.SettlementId = settlementId;
            entity.Id = expenseId;
            _context.Expenses.Update(entity);
            _context.ExpenseParticipations.RemoveRange(
                _context.ExpenseParticipations
                    .Where(p => p.ExpenseId == expenseId)
            );
            _context.ExpenseParticipations.AddRange(
                expense.Participants.Select(userId => new ExpenseParticipation
                {
                    ExpenseId = expenseId,
                    SettlementUserId = userId
                })
            );
            _context.SaveChanges();
        }
    }
}