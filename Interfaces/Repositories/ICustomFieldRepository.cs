using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Repositories
{
    public interface ICustomFieldRepository : IBaseRepository<CustomField2>
    {
        Task<List<CustomField2>> AllWithReferencesAsync();
        Task<List<CustomField2>> AllIncludingHiddenAsync();
        Task<CustomField2> FindWithReferencesAsync(int id);
        CustomField2 FindWithReferencesNoTracking(int id);
        Task<List<CustomField2>> AllWithValuesByTaskId(int projectTaskId);
    }
}
