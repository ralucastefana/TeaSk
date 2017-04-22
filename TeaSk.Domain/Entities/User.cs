using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaSk.Domain.Infrastructure;

namespace TeaSk.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
      
        public virtual ICollection<Skills> Skills { get; set; }

    }
}
