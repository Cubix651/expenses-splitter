using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesSplitter.WebApi.Database.Models
{
    public class Friend
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public virtual User User { get; set; }
        
     
    }
}
