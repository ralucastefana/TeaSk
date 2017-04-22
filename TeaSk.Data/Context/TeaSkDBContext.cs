using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaSk.Domain.Entities;
using TeaSk.Domain.Infrastructure;

namespace TeaSk.Data.Context
{
    public class TeaSkDBContext:DbContext
    {
        public TeaSkDBContext() : base("TeaSk")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Activities> Activities { get; set; }

        //public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        //{
        //    return base.Set<TEntity>();
        //}
    }

}
