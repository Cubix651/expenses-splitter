using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExpensesSplitter.WebApi.Database;
using ExpensesSplitter.WebApi.Database.Models;

namespace ExpensesSplitter.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SettlementsController : ControllerBase
    {
        private readonly ExpensesSplitterContext context;

        public SettlementsController(ExpensesSplitterContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Route("GetAllSettlements")]
        public ActionResult GetAllSettlements()
        {
            List<Settlement> settlements = context.Settlements.ToList();
            if (settlements.Count != 0)
            {
                return Ok(settlements);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("GetAllSettlementsByUser/")]
        public ActionResult GetAllSettlementsByUser(string id)
        {

            List<Settlement> settlements = context.Settlements.Where(x => x.IdOwner == id).ToList();
            if (settlements.Count != 0)
            {
                return Ok(settlements);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("GetAllSettlementsByUserParticipant/")]
        public ActionResult GetAllSettlementsByUserParticipant(string id)
        {
            List<SettlementUser> settlements = context.SettlementUsers.Where(x => x.UserId == id).ToList();
            var result = (from s in settlements
                          join u in context.Settlements on s.SettlementId equals u.Id
                          select new Settlement() { Id = u.Id, Name = u.Name, IdOwner = u.IdOwner}).ToList();
            //List<Settlement> settlements = context.Settlements.Where(x => x.IdOwner == id).ToList();
            if (result.Count != 0)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("GetSettlementUsers/")]
        public ActionResult GetSettlementUsers(string id)
        {

            List<SettlementUser> settlements = context.SettlementUsers.Where(x => x.SettlementId == id).ToList();
            var result = (from s in settlements
                         join u in context.Users on s.UserId equals u.Id
                         select new User() { Id = u.Id, Name = u.Name, Email = u.Email, Login = u.Login}).ToList();
            if (result.Count != 0)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("GetRole/")]
        public ActionResult GetRole(string user, string settlement)
        {

            var role = context.SettlementUsers.Where(x => x.UserId == user && x.SettlementId == settlement).FirstOrDefault();
            if (role != null)
            {
                return Ok(role);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("GetId")]
        public ActionResult GetId()
        {
            var settlements = context.Settlements.ToList().Count();
            settlements += 1;
            if (settlements != 0)
            {
                return Ok(settlements);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("GetSettlement/")]
        public ActionResult<Settlement> GetSettlement(string id)
        {

            var settlements = context.Settlements.Where(a => a.Id == id).FirstOrDefault();

            if (settlements != null)
            {
                return settlements;
            }

            return NotFound();
        }
        [NonAction]
        public User GetUser(string id)
        {
            var user = context.Users.Where(a => a.Id == id).FirstOrDefault();
            return user;
        }
        [HttpPost]
        [Route("AddUserToSettlement")]
        public ActionResult<SettlementUser> AddUserToSettlement(SettlementUser body)
        {
            var user = context.Users.Where(a => a.Login == body.UserId).FirstOrDefault();
            var id = GetSettlementUserId();
            var settlementUser = context.SettlementUsers;

                context.Add(new SettlementUser
                {
                  
                    SettlementId = body.SettlementId,
                    UserId = user.Id,
                    RoleId = "2"
                });
                context.SaveChanges();
                return Ok(settlementUser);

        }
            
        [NonAction]
        public int GetSettlementUserId()
        {
            var id = context.Settlements.ToList().Count();
            if(id == 0)
            {
                return 1;
            }
            else
            {
                return id + 1;
            }
        }
        [HttpPost]
        [Route("AddSettlement")]
        public ActionResult<Settlement> AddSettlement(Settlement settlement)
        {
            if (settlement.IdOwner != null)
            {
                var settlements = context.Settlements;
                int settlementUserId = GetSettlementUserId();
                //var user = GetUser(ownerId);
                //var user = context.Users.Where(a => a.Id == ownerId).FirstOrDefault();
                context.Add(new Settlement
                {
                    Id = settlement.Id,
                    Name = settlement.Name,
                    Description = settlement.Description,
                    IdOwner = settlement.IdOwner
                });
                context.Add(new SettlementUser
                {
                    //Id = settlementUserId.ToString(),
                    SettlementId = settlement.Id,
                    UserId = settlement.IdOwner,
                    RoleId = "1"
                });
                context.SaveChanges();
                return Ok(settlements);

            }
            else
            {
                var settlements = context.Settlements;
                context.Add(new Settlement
                {
                    Id = settlement.Id,
                    Name = settlement.Name,
                    Description = settlement.Description
                });
                context.SaveChanges();
                return Ok(settlements);
            }
        
            
        }
    }
}