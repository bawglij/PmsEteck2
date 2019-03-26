using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PmsEteck.Data.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly PmsEteckContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        protected DbSet<T> dbSet;
        protected IQueryable<T> builder;

        private DbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<T>();
                }
                return entities;
            }
        }

        public BaseService()
        {
            context = new PmsEteckContext();
            dbSet = context.Set<T>();
            builder = dbSet;
        }

        public PmsEteckContext GetContext()
        {
            return context;
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public BaseService<T> Include(Expression<Func<T, object>> expression)
        {
            Entities.Include(expression).Load();

            return this;
        }

        public T FindById(object id)
        {
            return Entities.Find(id);
        }

        public void Add(T entity, string userId = null)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                Entities.Add(entity);
                if(!string.IsNullOrWhiteSpace(userId))
                {
                    context.SaveChanges(userId);
                }
                context.SaveChanges();
                context.Entry(entity).GetDatabaseValues();
            }
            catch (Exception dbEx)
            {
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Update(T entity, string userId = null)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                context.Entry(entity).State = EntityState.Modified;
                if (!string.IsNullOrWhiteSpace(userId))
                    context.SaveChanges(userId);
                context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw new Exception(errorMessage, dbEx);
            }
        }

        public virtual void Delete(T entity, string userId = null)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                Entities.Remove(entity);
                if (!string.IsNullOrWhiteSpace(userId))
                    context.SaveChanges(userId);
                context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw new Exception(errorMessage, dbEx);
            }
        }
    }
}
