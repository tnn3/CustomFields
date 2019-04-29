using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomFields.DAL;
using Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomFieldRepository : CustomFieldRepository<CustomFieldInProject>, ICustomFieldRepository
    {
        protected DbContext RepositoryDbContext { get; set; }

        public CustomFieldRepository(ApplicationDbContext dataContext) : base(dataContext.CustomFields)
        {
            RepositoryDbContext = dataContext;
            RepositoryDbSet = RepositoryDbContext.Set<CustomFieldInProject>() ?? throw new NullReferenceException("CustomField DbSet was not found");
        }

        public Task<int> SaveChangesAsync()
        {
            return RepositoryDbContext.SaveChangesAsync();
        }

        public Task<List<CustomFieldInProject>> AllWithValuesByTaskId(int projectTaskId)
        {
            return RepositoryDbSet
                .Include(c => c.FieldName)
                .Include(c => c.CombinedFields)
                .Where(c => c.CombinedFields.Any(a => ((CustomFieldInTasks)a).ProjectTaskId  == projectTaskId))
                .ToListAsync();
        }
    }
}
