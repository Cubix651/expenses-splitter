using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesSplitter.WebApi.Database;
using ExpensesSplitter.WebApi.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesSplitter.WebApi.Controllers 
{
    [Route("api/group/")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly ExpensesSplitterContext context;

        public GroupsController(ExpensesSplitterContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("add")]
        public ActionResult<Group> AddGroup(Group body)
        {
            context.Add(new Group
            {
                Name = body.Name,
                SettlementId = body.SettlementId
            });
            context.SaveChanges();
            return Ok();

        }
        [HttpGet]
        [Route("get/")]
        public ActionResult GetGroupsFromSettlement(string id)
        {
            try
            {
                var result = context.Groups.Where(x => x.SettlementId == id).ToList();
                if (result.Count() != 0)
                {
                    return Ok(result);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"The file was not found: '{e}'");
            }
            return Ok();

        }
    }
}
