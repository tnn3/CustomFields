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
    public class CustomFieldRepository : CustomFieldRepository<CustomField2>, ICustomFieldRepository
    {
        protected DbContext RepositoryDbContext { get; set; }

        public CustomFieldRepository(ApplicationDbContext dataContext) : base(dataContext.CustomFields)
        {
            RepositoryDbContext = dataContext;
            RepositoryDbSet = RepositoryDbContext.Set<CustomField2>() ?? throw new NullReferenceException("CustomField DbSet was not found");
        }

        public Task<int> SaveChangesAsync()
        {
            return RepositoryDbContext.SaveChangesAsync();
        }

        public void Remove(CustomField2 customField)
        {
            RepositoryDbSet.Attach(customField);
            RepositoryDbContext.Entry(customField).State = EntityState.Deleted;
            RepositoryDbSet.Remove(customField);
        }

        public Task<List<CustomField2>> AllWithValuesByTaskId(int projectTaskId)
        {
            return RepositoryDbSet
                .Include(c => c.FieldName)
                .Include(c => c.CombinedFields)
                .Where(c => c.CombinedFields.Any(a => ((CustomFieldInTasks)a).ProjectTaskId  == projectTaskId))
                .ToListAsync();
        }
    }
}
