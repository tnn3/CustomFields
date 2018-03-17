using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbContext RepositoryDbContext { get; set; }
        protected DbSet<TEntity> RepositoryDbSet { get; set; }

        public BaseRepository(IDbContext dataContext)
        {
            RepositoryDbContext = dataContext as DbContext ?? throw new ArgumentNullException(nameof(dataContext));
            RepositoryDbSet = RepositoryDbContext.Set<TEntity>() ?? throw new NullReferenceException($"DbSet for {nameof(TEntity)} was not found.");
        }

        public virtual IEnumerable<TEntity> All() => RepositoryDbSet.ToList();

        public virtual async Task<IEnumerable<TEntity>> AllAsync() => await RepositoryDbSet.ToListAsync();

        public virtual TEntity Find(params object[] id)
        {
            return RepositoryDbSet.Find(id);
        }

        public virtual Task<TEntity> FindAsync(params object[] id)
        {
            return RepositoryDbSet.FindAsync(id);
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null) throw new InvalidOperationException("Unable to add a null entity to the repository.");
            RepositoryDbSet.Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null) throw new InvalidOperationException("Unable to add a null entity to the repository.");
            await RepositoryDbSet.AddAsync(entity);
        }

        public virtual TEntity Update(TEntity entity) => RepositoryDbSet.Update(entity).Entity;

        public virtual int SaveChanges()
        {
            CheckDisposed();
            return RepositoryDbContext.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            CheckDisposed();
            return RepositoryDbContext.SaveChangesAsync();
        }

        public virtual void Remove(TEntity entity)
        {
            RepositoryDbSet.Attach(entity);
            RepositoryDbContext.Entry(entity).State = EntityState.Deleted;
            RepositoryDbSet.Remove(entity);
        }

        public bool Exists(int id) => RepositoryDbSet.Find(id) != null;

        #region IDisposable Implementation

        private bool _isDisposed;

        protected void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException("Already disposed and cannot be used anymore.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (RepositoryDbContext != null)
                    {
                        RepositoryDbContext.Dispose();
                        RepositoryDbContext = null;
                    }
                }
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}
