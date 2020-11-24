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
        [Route("AddSettlement")]
        public ActionResult<Settlement> AddSettlement(Settlement settlement)
        {
            if (settlement.IdOwner != null)
            {
                var settlements = context.Settlements;
                //var user = GetUser(ownerId);
                //var user = context.Users.Where(a => a.Id == ownerId).FirstOrDefault();
                context.Add(new Settlement
                {
                    Id = settlement.Id,
                    Name = settlement.Name,
                    Description = settlement.Description,
                    IdOwner = settlement.IdOwner
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