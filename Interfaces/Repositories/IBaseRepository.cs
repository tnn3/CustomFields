using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> AllAsync();
        Task<TEntity> FindAsync(params object[] id);
        Task AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        Task<int> SaveChangesAsync();
        void Remove(TEntity entity);
        bool Exists(int id);
    }
}
