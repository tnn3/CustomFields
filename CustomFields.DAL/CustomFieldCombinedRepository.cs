using System;
using System.Collections.Generic;
using System.Linq;
using CustomFields.Domain;
using CustomFields.Interfaces;
using CustomFields.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomFields.DAL
{
    public class CustomFieldCombinedRepository : ICustomFieldCombinedRepository
    {
        protected DbContext RepositoryDbContext { get; set; }
        protected DbSet<CustomFieldCombined> RepositoryDbSet { get; set; }

        public CustomFieldCombinedRepository(IDbContext dataContext)
        {
            RepositoryDbContext = dataContext as DbContext ?? throw new ArgumentNullException(nameof(dataContext));
            RepositoryDbSet = RepositoryDbContext.Set<CustomFieldCombined>() ?? throw new NullReferenceException($"DbSet for {nameof(CustomFieldCombined)} was not found.");
        }

        public IEnumerable<CustomFieldCombined> All()
        {
            return RepositoryDbSet.ToList();
        }

        public CustomFieldCombined Find(params object[] id)
        {
            return RepositoryDbSet.Find(id);
        }
    }
}
