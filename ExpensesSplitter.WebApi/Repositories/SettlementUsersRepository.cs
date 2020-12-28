using System;
using System.Collections.Generic;
using System.Linq;
using ExpensesSplitter.WebApi.Database;
using ExpensesSplitter.WebApi.Models;
using AutoMapper;

namespace ExpensesSplitter.WebApi.Repositories
{
    public interface ISettlementUsersRepository
    {
        IEnumerable<SettlementUser> GetSettlementUsers(string settlementId);
        SettlementUser GetSettlementUser(string settlementId, Guid settlementUserId);
    }

    public class SettlementUsersRepository : ISettlementUsersRepository
    {
        private readonly ExpensesSplitterContext _context;
        private readonly IMapper _mapper;

        public SettlementUsersRepository(ExpensesSplitterContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<SettlementUser> GetSettlementUsers(string settlementId)
        {
            return _context.SettlementUsers
                .Where(u => u.SettlementId == settlementId)
                .Select(u => _mapper.Map<SettlementUser>(u));
        }

        public SettlementUser GetSettlementUser(string settlementId, Guid settlementUserId)
        {
            var user = _context.SettlementUsers
                .First(u => u.Id == settlementUserId && u.SettlementId == settlementId);
            return _mapper.Map<SettlementUser>(user);
        }
    }
}