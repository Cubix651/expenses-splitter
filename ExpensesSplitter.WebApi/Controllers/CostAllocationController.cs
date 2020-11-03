using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExpensesSplitter.WebApi.Models;
using System.Collections.Immutable;

namespace ExpensesSplitter.WebApi.Controllers
{
    
    [ApiController]
    [Route("api/")]
    public class CostAllocationController : ControllerBase
    {
        private static int AutoIncrement = 9;
        private static List<CostAllocationTest> costAllocations = new List<CostAllocationTest>()
        {
            new CostAllocationTest { Id = 1, Name = "Wyjazd w Bieszczady" },
            new CostAllocationTest { Id = 2, Name = "Wycieczka szkolna" },
            new CostAllocationTest { Id = 3, Name = "Rejs" },
            new CostAllocationTest { Id = 4, Name = "Wyjazd do Warszawy" },
            new CostAllocationTest { Id = 5, Name = "Biwak" },
            new CostAllocationTest { Id = 6, Name = "Wyjazd w Góry" },
            new CostAllocationTest { Id = 7, Name = "Wyjazd nad Morze" },
            new CostAllocationTest { Id = 8, Name = "Wyjazd do Niemiec" }
        };

        public CostAllocationController()
        {
           /* costAllocations.Add(new CostAllocationTest { Id = 1, Name = "Wyjazd w Bieszczady" });
            costAllocations.Add(new CostAllocationTest { Id = 2, Name = "Wycieczka szkolna" });
            costAllocations.Add(new CostAllocationTest { Id = 3, Name = "Rejs" });
            costAllocations.Add(new CostAllocationTest { Id = 4, Name = "Wyjazd do Warszawy" });
            costAllocations.Add(new CostAllocationTest { Id = 5, Name = "Biwak" });
            costAllocations.Add(new CostAllocationTest { Id = 6, Name = "Wyjazd w Góry" });
            costAllocations.Add(new CostAllocationTest { Id = 7, Name = "Wyjazd nad Morze" });
            costAllocations.Add(new CostAllocationTest { Id = 8, Name = "Wyjazd do Niemiec" }); */
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
        [Route("GetCostAllocation")]
        public ActionResult<CostAllocationTest> GetCostAllocation(int Id)
        {

            var costAllocation = costAllocations.Find(x => x.Id.Equals(Id));

            if (costAllocation != null)
            {
                return costAllocation;
            }

            return NotFound();
        }
        [Route("AddCostAllocation")]
        [HttpPost]
        public ActionResult AddCostAllocation(string Name)
        {
            var costAllocation = costAllocations.Find(x => x.Id.Equals(AutoIncrement));

            if (costAllocation != null)
            {
                return Ok("Id is already used");

            }
            costAllocations.Add(new CostAllocationTest { Id = AutoIncrement, Name = Name });
            AutoIncrement++;
           return Ok(costAllocations);
        }
    }
}
