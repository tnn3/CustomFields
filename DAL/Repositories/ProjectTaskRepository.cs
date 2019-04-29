using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Interfaces;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProjectTaskRepository : BaseRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<ProjectTask>> AllAsyncWithReferences()
        {
            return await RepositoryDbSet
                .Include(p => p.CustomFields)
                .ToListAsync();
        }

        public async Task<ProjectTask> FindAsyncWithReferences(int id)
        {
            return await RepositoryDbSet
                .Include(ct => ct.CustomFields)
                    .ThenInclude(c => c.CustomField)
                        .ThenInclude(cf => cf.FieldName)
                            .ThenInclude(fieldName => fieldName.FieldNameTranslations)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
