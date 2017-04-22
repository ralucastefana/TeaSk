using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaSk.Domain.Infrastructure;

namespace TeaSk.Domain.Entities
{
    public class Level : BaseEntity
    {
        public string Name { get; set; }
        public string Limit { get; set; }
    }
}
