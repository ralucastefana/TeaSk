using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaSk.Data.Context;

namespace TeaSk.Data.Infrastructures //singleton
{
   public class DatabaseFactory : IDatabaseFactory
    {
        private DbContext _dataContext;
        public DbContext Get()
        {
            return _dataContext ?? (_dataContext = new TeaSkDBContext());
        }

        public void Dispose()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
