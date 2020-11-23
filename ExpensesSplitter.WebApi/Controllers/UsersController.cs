using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExpensesSplitter.WebApi.Database;
using ExpensesSplitter.WebApi.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesSplitter.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ExpensesSplitterContext context;
        public UsersController(ExpensesSplitterContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody]User user)
        {   
            var userLogin = context.Users.Where(a => a.Login == user.Login).FirstOrDefault();
            if (userLogin == null)
            {
                return BadRequest("Invalid client request");
            }
            if(user.Password == userLogin.Password)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5000",
                    audience:"https://localhost:500",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                //return Ok(new { Token = tokenString });
                return Ok(userLogin);
            }
            return Unauthorized();
        }
        [HttpGet]
        [Route("GetUserId")]
        public ActionResult GetId()
        {
            var users = context.Users.ToList().Count();
            users += 1;
            if (users != 0)
            {
                return Ok(users);
            }
            return Ok(1);
        }
        [HttpPost]
        [Route("register")]
        public ActionResult Register(User user)
        {
            context.Add(new User
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email
            });
            context.SaveChanges();
            return (Ok(user));
        }
    }
}
