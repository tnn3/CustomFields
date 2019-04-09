using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomFields.Interfaces
{
    public interface ICustomFieldRepository<TCustomField> where TCustomField : ICustomField
    {
        Task<List<TCustomField>> AllAsync();
        Task<TCustomField> FindAsync(params object[] id);
        Task AddAsync(TCustomField customField);
        TCustomField Update(TCustomField customField);
        bool Exists(int id);
        Task<List<TCustomField>> AllWithReferencesAsync();
        Task<List<TCustomField>> AllIncludingHiddenAsync();
        Task<TCustomField> FindWithReferencesAsync(int id);
        TCustomField FindWithReferencesNoTracking(int id);
    }
}
