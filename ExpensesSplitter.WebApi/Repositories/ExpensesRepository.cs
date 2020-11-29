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
        void CreateExpense(string settlementId, Expense expense);
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

        public void CreateExpense(string settlementId, Expense expense)
        {
            var entity = _mapper.Map<Database.Models.Expense>(expense);
            entity.SettlementId = settlementId;
            _context.Expenses.Add(entity);
            _context.SaveChanges();
        }
    }
}