using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaSk.Domain.Entities;
using TeaSk.Domain.Infrastructure;

namespace TeaSk.Data.Context //reprezentarea bazei de date
{
    public class TeaSkDBContext:DbContext
    {
        public TeaSkDBContext() : base("Data Source=tcp:192.168.0.104,49172;Initial Catalog=TeaSk;Integrated Security=True")
        {
        }

        public DbSet<User> Users { get; set; }  //setter
        public DbSet<Level> Levels { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Activities> Activities { get; set; }

        //public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        //{
        //    return base.Set<TEntity>();
        //}
    }

}
