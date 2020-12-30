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
        Guid CreateSettlementUser(string settlementId, NewSettlementUser settlementUser);
        void DeleteSettlementUser(string settlementId, Guid settlementUserId);
        void UpdateSettlementUser(string settlementId, Guid settlementUserId, NewSettlementUser settlementUser);
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
                .ToList()
                .Select(u => _mapper.Map<SettlementUser>(u));
        }

        public SettlementUser GetSettlementUser(string settlementId, Guid settlementUserId)
        {
            var user = _context.SettlementUsers
                .First(u => u.Id == settlementUserId && u.SettlementId == settlementId);
            return _mapper.Map<SettlementUser>(user);
        }

        public Guid CreateSettlementUser(string settlementId, NewSettlementUser settlementUser)
        {
            var entity = _mapper.Map<Database.Models.SettlementUser>(settlementUser);
            entity.SettlementId = settlementId;
            _context.SettlementUsers.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void DeleteSettlementUser(string settlementId, Guid settlementUserId)
        {
            var entity = _context.SettlementUsers
                .First(u => u.SettlementId == settlementId && u.Id == settlementUserId);
            _context.SettlementUsers.Remove(entity);
            _context.SaveChanges();
        }

        public void UpdateSettlementUser(string settlementId, Guid settlementUserId, NewSettlementUser settlementUser)
        {
            var entity = _mapper.Map<Database.Models.SettlementUser>(settlementUser);
            entity.SettlementId = settlementId;
            entity.Id = settlementUserId;
            _context.SettlementUsers.Update(entity);
            _context.SaveChanges();
        }
    }
}