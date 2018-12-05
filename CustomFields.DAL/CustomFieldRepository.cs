using System;
using System.Collections.Generic;
using System.Linq;
using CustomFields.Domain;
using CustomFields.Interfaces;
using CustomFields.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomFields.DAL
{
    public class CustomFieldRepository : ICustomFieldRepository
    {
        protected DbContext RepositoryDbContext { get; set; }
        protected DbSet<CustomField> RepositoryDbSet { get; set; }

        public CustomFieldRepository(IDbContext dataContext)
        {
            RepositoryDbContext = dataContext as DbContext ?? throw new ArgumentNullException(nameof(dataContext));
            RepositoryDbSet = RepositoryDbContext.Set<CustomField>() ?? throw new NullReferenceException($"DbSet for {nameof(CustomField)} was not found.");
        }

        public IEnumerable<CustomField> All()
        {
            return RepositoryDbSet.ToList();
        }

        public CustomField Find(params object[] id)
        {
            return RepositoryDbSet.Find(id);
        }
    }
}
