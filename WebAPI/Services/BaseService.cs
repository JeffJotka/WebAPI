using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using WebAPI.DatabaseContext;
using WebAPI.Extensions;

namespace WebAPI.Services
{
    public abstract class BaseService
    {
        protected WebApiDbContext Context { get; }
        protected IMapper Mapper { get; }

        #region BaseService()
        public BaseService(WebApiDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        #endregion

        #region Find()
        public virtual TEntity Find<TEntity>(long id) where TEntity : class
        {
            return Context.Find<TEntity>(id);
        }
        #endregion

       #region Create()
        public virtual TEntity Create<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Add(entity);
            SaveChanges();

            return entity;
        }

        public virtual List<TEntity> Create<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities.Length > 0)
            {
                Context.AddRange(entities);
                SaveChanges();
            }

            return new List<TEntity>(entities);
        }
       #endregion

        #region SaveChanges()
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return Context.SaveChanges(acceptAllChangesOnSuccess);
        }
        #endregion

        #region SaveChangesAsync()
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        #endregion

        #region Update()
        public virtual TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    Context.Update(entity);
                }

                SaveChanges();
            }

            return entity;
        }

        public virtual List<TEntity> Update<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities.Length > 0)
            {
                Context.UpdateRange(entities.Where(p => Context.Entry(p).State == EntityState.Detached));
                SaveChanges();
            }

            return new List<TEntity>(entities);
        }
        #endregion

        #region Remove()
        public virtual TEntity Remove<TEntity>(long id) where TEntity : class  
        {
            return Remove(Find<TEntity>(id));
        }

        public virtual TEntity Remove<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                Context.Remove(entity);
                SaveChanges();
            }

            return entity;
        }

        public virtual List<TEntity> Remove<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities.Length > 0)
            {
                Context.RemoveRange(entities);
                SaveChanges();
            }

            return new List<TEntity>(entities);
        }
        #endregion
    }
}