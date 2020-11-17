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

        private static List<Settlement> costAllocations = new List<Settlement>()
        {
            new Settlement { Id = "1", Name = "Wyjazd w Bieszczady" },
            new Settlement { Id = "2", Name = "Wycieczka szkolna" },
            new Settlement { Id = "3", Name = "Rejs" },
            new Settlement { Id = "4", Name = "Wyjazd do Warszawy" },
            new Settlement { Id = "5", Name = "Biwak" },
            new Settlement { Id = "6", Name = "Wyjazd w Góry" },
            new Settlement { Id = "7", Name = "Wyjazd nad Morze" },
            new Settlement { Id = "8", Name = "Wyjazd do Niemiec" }
        };
        public SettlementsController(ExpensesSplitterContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Route("GetCostAllocations")]
        public ActionResult GetCostAllocations()
        {
            if (costAllocations.Count != 0)
                return Ok(costAllocations);

            return NotFound();
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
        [HttpPost]
        [Route("AddSettlement")]
        public ActionResult<Settlement> AddSettlement(Settlement settlement)
        {
            var settlements = context.Settlements;
            context.Add(new Settlement
            {
                Id= settlement.Id,
                Name = settlement.Name,
                Description = settlement.Description
            });
            context.SaveChanges();
            return Ok(settlements);
        }
    }
}