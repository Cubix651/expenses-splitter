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
        public ActionResult<Friend> FindFriend(string id, string name)
        {
            var entity = context.Users.Where(x => x.Name == name || x.Email == name).FirstOrDefault();
            context.Add(new Friend
            {
                UserId = id,
                FriendId = entity.Id
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
                List<Friend> friends = context.Friends.Where(x => x.UserId == id).ToList();
                var result = (from f in friends
                              join u in context.Users on f.FriendId equals u.Id                            
                              select new { Id = u.Id, Name = u.Name, Email = u.Email, Login = u.Login }).ToList();
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
        public ActionResult GetFriends(string id, string friend)
        {
            var result = context.Friends.Where(x => x.UserId == id && x.FriendId == friend).FirstOrDefault();
            context.Friends.Remove(result);
            return Ok();

        }
    }
}
