﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesSplitter.WebApi.Database.Models
{
    public class SettlementUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string SettlementId { get; set; }
        public string DisplayName { get; set; }
        public virtual ICollection<ExpenseParticipation> Participations { get; set; }
        public enum Role { Admin, Watcher, Editor }
        public Role RoleId { get; set; }
        public Guid GroupId { get; set; }
        //public virtual Group Group { get; set;}
    }
}
