using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Repositories
{
    public interface ICustomFieldRepository : IBaseRepository<CustomField>
    {
        Task<List<CustomField>> AllWithReferencesAsync();
        Task<List<CustomField>> AllIncludingHiddenAsync();
        Task<CustomField> FindWithReferencesAsync(int id);
        CustomField FindWithReferencesNoTracking(int id);
        Task<List<CustomField>> AllWithValuesByTaskId(int projectTaskId);
    }
}
