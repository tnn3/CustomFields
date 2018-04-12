using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();
        TEntity Find(params object[] id);
        Task<TEntity> FindAsync(params object[] id);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Remove(TEntity entity);
        bool Exists(int id);
    }
}
