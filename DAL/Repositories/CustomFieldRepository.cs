using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Domain;
using Domain.Enums;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomFieldRepository : BaseRepository<CustomField>, ICustomFieldRepository
    {
        public CustomFieldRepository(IDbContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<CustomField>> AllAsync()
        {
            return await RepositoryDbSet
                .Where(c => c.Status != FieldStatus.Hidden)
                .ToListAsync();
        }

        public Task<List<CustomField>> AllWithReferencesAsync()
        {
            return RepositoryDbSet
                .Where(c => c.Status != FieldStatus.Hidden)
                .Include(c => c.Tasks)
                .ToListAsync();
        }

        public Task<List<CustomField>> AllIncludingHiddenAsync()
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

        public CustomField FindWithReferencesNoTracking(int id)
        {
            return RepositoryDbSet
                .Include(c => c.Tasks)
                .AsNoTracking()
                .SingleOrDefault(c => c.Id == id);
        }

        public Task<List<CustomField>> AllWithValuesByTaskId(int projectTaskId)
        {
            return RepositoryDbSet
                .Include(c => c.Tasks)
                .Where(c => c.Tasks.Any(a => a.ProjectTaskId == projectTaskId))
                .ToListAsync();
        }
    }
}
