using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaSk.Domain.Infrastructure;

namespace TeaSk.Data.Infrastructures
{
   public interface IUnitOfWork
    {
        void Dispose();
        void Save();
        void Dispose(bool disposing);
        Repository<T> Repository<T>() where T : BaseEntity;
    }
}
