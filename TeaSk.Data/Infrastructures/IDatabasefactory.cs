using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TeaSk.Data.Infrastructures
{
    public interface IDatabaseFactory //interfata singleton (pentru abstractizare)
    {
        DbContext Get();
    }
}
