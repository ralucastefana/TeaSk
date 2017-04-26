using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeaSk.Web.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
    }
}