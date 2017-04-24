using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeaSk.Data.Infrastructures;
using TeaSk.Domain.Infrastructure;
using Omu.ValueInjecter;

namespace TeaSk.Application.Infrastructure
{
    public class Service<T> : IService<T> where T : BaseEntity, new()
    {
        protected readonly IRepository<T> Repository;
        protected readonly IUnitOfWork UnitOfWork;

        public Service(IRepository<T> repository, IUnitOfWork unitOfWorK)
        {
            this.Repository = repository;
            this.UnitOfWork = unitOfWorK;
        }

        public T GetByID(int id)
        {
            return (id == 0) ? new T() : Repository.GetById(id);
        }

        public T GetFirst()
        {
            return GetFirst(t => true);
        }

        public T GetFirst(Expression<Func<T, bool>> whereClause)
        {
            return (T)(from x in Repository.Query(whereClause) select new T().InjectFrom(x)).FirstOrDefault();
        }

        public List<T> GetMany(Expression<Func<T, bool>> whereClause)
        {
            return (from x in Repository.Query(whereClause) select new T().InjectFrom(x)).Cast<T>().ToList();
        }

        public List<T> GetAll()
        {
            return Repository.Table.ToList();
        }

        public virtual T Add(T entity)
        {
            var dbEntity = new T();
            dbEntity.InjectFrom(entity);

            Repository.Insert(dbEntity);
            Save();

            return (T)entity.InjectFrom(dbEntity);
        }

        public virtual void AddList(List<T> entities)
        {
            foreach (var entity in entities)
            {
                var dbEntity = new T();
                dbEntity.InjectFrom(entity);

                Repository.Insert(dbEntity);
            }

            Save();
        }
        
        public virtual List<T> AddEntities(List<T> entities)
        {
            var addedEntities = new List<T>();

            foreach (var entity in entities)
            {
                var dbEntity = new T();
                dbEntity.InjectFrom(entity);

                Repository.Insert(dbEntity);
                addedEntities.Add(dbEntity);
            }

            Save();
            return addedEntities;
        }

        public virtual T Update(T entity)
        {
            var dbEntity = Repository.GetById(entity.Id);
            dbEntity.InjectFrom(entity);

            Repository.Update(dbEntity);
            Save();

            return (T)entity.InjectFrom(dbEntity);
        }

        public virtual void Delete(T entity)
        {
            var dbEntity = Repository.GetById(entity.Id);

            Repository.Delete(dbEntity);
            Save();
        }

        protected void Save()
        {
            UnitOfWork.Save();
        }
    }
}
