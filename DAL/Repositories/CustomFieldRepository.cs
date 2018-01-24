using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Domain;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomFieldRepository : BaseRepository<CustomField>, ICustomFieldRepository
    {
        public CustomFieldRepository(IDbContext dataContext) : base(dataContext)
        {
        }

        public Task<List<CustomField>> AllWithReferencesAsync()
        {
            return RepositoryDbSet
                .Include(c => c.Tasks)
                .ToListAsync();
        }

        public Task<CustomField> FindWithReferencesAsync(int id)
        {
            return RepositoryDbSet
                .Include(c => c.Tasks)
                .SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
