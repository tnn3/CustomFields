using System.Collections.Generic;
using System.Threading.Tasks;
using CustomFields.Domain;

namespace CustomFields.Interfaces.Repositories
{
    public interface ICustomFieldRepository
    {
        IEnumerable<CustomField> All();
        //Task<IEnumerable<CustomField>> AllAsync();
        CustomField Find(params object[] id);
        //Task<CustomField> FindAsync(params object[] id);
        //void Add(CustomField entity);
        //Task AddAsync(CustomField entity);
        //CustomField Update(CustomField entity);
        //int SaveChanges();
        //Task<int> SaveChangesAsync();
        //void Remove(CustomField entity);
        //bool Exists(int id);

        //Task<List<CustomField>> AllWithReferencesAsync();
        //Task<List<CustomField>> AllIncludingHiddenAsync();
        //Task<CustomField> FindWithReferencesAsync(int id);
        //CustomField FindWithReferencesNoTracking(int id);
        //Task<List<CustomField>> AllWithValuesByTaskId(int projectTaskId);
    }
}
