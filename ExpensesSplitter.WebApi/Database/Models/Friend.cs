using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesSplitter.WebApi.Database.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class Friend : ControllerBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public virtual User User { get; set; }
    }
}
