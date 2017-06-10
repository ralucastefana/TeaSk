using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Omu.ValueInjecter;
using TeaSk.Domain.Infrastructure;

namespace TeaSk.Data.Infrastructures
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private DbContext _context;
        private IDbSet<T> dbset;
        string _errorMessage = string.Empty;

        protected IDatabaseFactory DataBaseFactory //singleton
        {
            get;
            private set;
        }

        public Repository(IDatabaseFactory databaseFactory)
        {
            DataBaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected DbContext DataContext => _context ?? (_context = DataBaseFactory.Get());

        private IDbSet<T> Entities => dbset ?? (dbset = _context.Set<T>());

        public T GetById(int id)
        {
            return Entities.Find(id);
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> where)
        {
            return Entities.Where(where).ToList();
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                      $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                Entities.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage += Environment.NewLine +
                                         $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }

                throw new Exception(_errorMessage, dbEx);
            }
        }
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage += Environment.NewLine +
                                    $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public virtual IQueryable<T> Table => Entities;
    }
}