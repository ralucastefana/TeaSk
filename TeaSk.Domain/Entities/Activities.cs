using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaSk.Domain.Infrastructure;

namespace TeaSk.Domain.Entities
{
    public class Activities : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
    }
}
