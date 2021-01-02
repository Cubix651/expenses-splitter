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
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly ExpensesSplitterContext context;
        public FriendsController(ExpensesSplitterContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("add")]
        public ActionResult<Friend> AddFriend(Friend body)
        {
            context.Add(new Friend
            {
                UserId = body.UserId,
                FriendId = body.FriendId
            });
            context.SaveChanges();
            return Ok();

        }
        [HttpGet]
        [Route("get/")]
        public ActionResult GetFriends(string id)
        {
            try
            {
                var result = context.Friends.Where(x => x.UserId == id).ToList();
                if (result.Count() != 0)
                {
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"The file was not found: '{e}'");
            }
            return Ok();

        }
        [HttpGet]
        [Route("remove/")]
        public ActionResult RemoveFriend(string id, string friend)
        {
                var result = context.Friends.Where(x => x.UserId == id || x.FriendId == friend).FirstOrDefault();
                context.Friends.Remove(result);
                return Ok(result);
        }
    }
}
