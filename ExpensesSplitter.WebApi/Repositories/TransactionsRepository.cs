using System;
using System.Collections.Generic;
using System.Linq;
using ExpensesSplitter.WebApi.Database;
using ExpensesSplitter.WebApi.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ExpensesSplitter.WebApi.Repositories
{
    public interface ITransactionsRepository
    {
        IEnumerable<Transaction> GetTransactions(string settlementId);
        Transaction GetTransaction(string settlementId, Guid transactionId);
        Guid CreateTransaction(string settlementId, NewTransaction transaction);
        void DeleteTransaction(string settlementId, Guid transactionId);
        void UpdateTransaction(string settlementId, Guid transactionId, NewTransaction transaction);
    }

    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ExpensesSplitterContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionsRepository> _logger;

        public TransactionsRepository(ExpensesSplitterContext context,
            IMapper mapper,
            ILogger<TransactionsRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<Transaction> GetTransactions(string settlementId)
        {
            var entities = _context.Transactions
                .Where(t => t.SettlementId == settlementId)
                .OrderByDescending(t => t.DateTime);
            return _mapper.ProjectTo<Transaction>(entities);
        }

        public Transaction GetTransaction(string settlementId, Guid transactionId)
        {
            var entity = _context.Transactions
                .Where(t => t.SettlementId == settlementId)
                .First(t => t.Id == transactionId);
            return _mapper.Map<Transaction>(entity);
        }

        public Guid CreateTransaction(string settlementId, NewTransaction transaction)
        {
            var entity = _mapper.Map<Database.Models.Transaction>(transaction);
            entity.SettlementId = settlementId;
            _context.Transactions.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void DeleteTransaction(string settlementId, Guid transactionId)
        {
            var entity = _context.Transactions
                .Where(t => t.SettlementId == settlementId)
                .First(t => t.Id == transactionId);
            _context.Transactions.Remove(entity);
            _context.SaveChanges();
        }

        public void UpdateTransaction(string settlementId, Guid transactionId, NewTransaction transaction)
        {
            var entity = _mapper.Map<Database.Models.Transaction>(transaction);
            entity.SettlementId = settlementId;
            entity.Id = transactionId;
            _context.Transactions.Update(entity);
            _context.SaveChanges();
        }
    }
}