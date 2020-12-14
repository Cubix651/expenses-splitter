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
        public ActionResult<Group> AddUserToSettlement(Group body)
        {
            context.Add(new Group
            {
                Name = body.Name,
                SettlementId = body.SettlementId
            });
            context.SaveChanges();
            return Ok();

        }
    }
}
