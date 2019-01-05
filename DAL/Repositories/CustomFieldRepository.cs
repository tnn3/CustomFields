using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Domain;
using CustomFields.Domain.Enums;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomFieldRepository : BaseRepository<CustomField2>, ICustomFieldRepository
    {
        public CustomFieldRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }

        public override async Task<List<CustomField2>> AllAsync()
        {
            return await RepositoryDbSet
                .Where(c => c.Status != FieldStatus.Hidden)
                .ToListAsync();
        }

        public Task<List<CustomField2>> AllWithReferencesAsync()
        {
            return RepositoryDbSet
                .Where(c => c.Status != FieldStatus.Hidden)
                .Include(c => c.CombinedFields)
                .ToListAsync();
        }

        public Task<List<CustomField2>> AllIncludingHiddenAsync()
        {
            return RepositoryDbSet
                .Include(c => c.CombinedFields)
                .ToListAsync();
        }

        public Task<CustomField2> FindWithReferencesAsync(int id)
        {
            return RepositoryDbSet
                .Include(c => c.CombinedFields)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public CustomField2 FindWithReferencesNoTracking(int id)
        {
            return RepositoryDbSet
                .Include(c => c.CombinedFields)
                .AsNoTracking()
                .SingleOrDefault(c => c.Id == id);
        }

        public Task<List<CustomField2>> AllWithValuesByTaskId(int projectTaskId)
        {
            return RepositoryDbSet
                .Include(c => c.CombinedFields)
                .Where(c => c.CombinedFields.Any(a => ((CustomFieldInTasks)a).ProjectTaskId  == projectTaskId))
                .ToListAsync();
        }
    }
}
