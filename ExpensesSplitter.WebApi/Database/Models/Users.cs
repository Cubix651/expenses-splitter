﻿namespace ExpensesSplitter.WebApi.Database.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}