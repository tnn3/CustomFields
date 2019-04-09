using System.Collections.Generic;
using System.Threading.Tasks;
using CustomFields.Interfaces;
using Domain;

namespace Interfaces.Repositories
{
    public interface ICustomFieldRepository : ICustomFieldRepository<CustomFieldInProject>
    {
        Task<int> SaveChangesAsync();
        void Remove(CustomFieldInProject customField);
        Task<List<CustomFieldInProject>> AllWithValuesByTaskId(int projectTaskId);
    }
}
