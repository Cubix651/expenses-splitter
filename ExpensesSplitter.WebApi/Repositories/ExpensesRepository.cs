using System;
using System.Collections.Generic;
using System.Linq;
using ExpensesSplitter.WebApi.Database;
using ExpensesSplitter.WebApi.Models;
using AutoMapper;

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

        public ExpensesRepository(ExpensesSplitterContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Expense> GetExpenses(string settlementId)
        {
            return _context.Expenses
                .Where(e => e.SettlementId == settlementId)
                .Select(e => _mapper.Map<Expense>(e));
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
            var entity = _mapper.Map<Database.Models.Expense>(expense);
            entity.SettlementId = settlementId;
            _context.Expenses.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void DeleteExpense(string settlementId, Guid expenseId)
        {
            var entity = _context.Expenses
                .Where(e => e.SettlementId == settlementId)
                .First(e => e.Id == expenseId);
            _context.Expenses.Remove(entity);
            _context.SaveChanges();
        }

        public void UpdateExpense(string settlementId, Guid expenseId, NewExpense expense)
        {
            var entity = _mapper.Map<Database.Models.Expense>(expense);
            entity.SettlementId = settlementId;
            entity.Id = expenseId;
            _context.Expenses.Update(entity);
            _context.SaveChanges();
        }
    }
}