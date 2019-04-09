using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomFields.Domain;
using CustomFields.Domain.Enums;
using CustomFields.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomFields.DAL
{
    public class CustomFieldRepository<TCustomField> : ICustomFieldRepository<TCustomField> where TCustomField : CustomField
    {
        protected DbSet<TCustomField> RepositoryDbSet { get; set; }

        public CustomFieldRepository(DbSet<TCustomField> repositoryDbSet)
        {
            RepositoryDbSet = repositoryDbSet ?? throw new NullReferenceException("CustomField DbSet was not found");
        }

        public virtual async Task<List<TCustomField>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(c => c.FieldName)
                .ThenInclude(fieldName => fieldName.FieldNameTranslations)
                .Where(c => c.Status != FieldStatus.Hidden)
                .ToListAsync();
        }

        public virtual Task<TCustomField> FindAsync(params object[] id)
        {
            return RepositoryDbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TCustomField customField)
        {
            if (customField == null) throw new InvalidOperationException("Unable to add a null customField to the repository.");
            await RepositoryDbSet.AddAsync(customField);
        }

        public virtual TCustomField Update(TCustomField customField)
        {
            return RepositoryDbSet.Update(customField).Entity;
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Find(id) != null;
        }

        public Task<List<TCustomField>> AllWithReferencesAsync()
        {
            return RepositoryDbSet
                .Include(c => c.FieldName)
                .ThenInclude(fieldName => fieldName.FieldNameTranslations)
                .Where(c => c.Status != FieldStatus.Hidden)
                .Include(c => c.CombinedFields)
                .ToListAsync();
        }

        public Task<List<TCustomField>> AllIncludingHiddenAsync()
        {
            return RepositoryDbSet
                .Include(c => c.FieldName)
                .ThenInclude(fieldName => fieldName.FieldNameTranslations)
                .Include(c => c.CombinedFields)
                .ToListAsync();
        }

        public Task<TCustomField> FindWithReferencesAsync(int id)
        {
            return RepositoryDbSet
                .Include(c => c.FieldName)
                .Include(c => c.CombinedFields)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public TCustomField FindWithReferencesNoTracking(int id)
        {
            return RepositoryDbSet
                .Include(c => c.FieldName)
                .Include(c => c.CombinedFields)
                .AsNoTracking()
                .SingleOrDefault(c => c.Id == id);
        }
    }
}
