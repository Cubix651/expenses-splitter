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
        [Route("GetSettlement")]
        public ActionResult<Settlement> GetSettlement(string id)
        {

            var settlements = context.Settlements.Where(a => a.Id == id).FirstOrDefault();

            if (settlements != null)
            {
                return settlements;
            }

            return NotFound();
        }
        [HttpPost]
        [Route("AddSettlement")]
        public ActionResult<Settlement> AddSettlement(Settlement settlement)
        {

            context.Settlements.Add(new Settlement
            {
                Name = settlement.Name,
                Description = settlement.Description
            });
            context.SaveChanges();
            return Ok(settlement);
        }
    }
}
