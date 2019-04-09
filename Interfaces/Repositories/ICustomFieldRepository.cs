using System.Collections.Generic;
using System.Threading.Tasks;
using CustomFields.Interfaces;
using Domain;

namespace Interfaces.Repositories
{
    public interface ICustomFieldRepository : ICustomFieldRepository<CustomField2>
    {
        Task<int> SaveChangesAsync();
        void Remove(CustomField2 customField);
        Task<List<CustomField2>> AllWithValuesByTaskId(int projectTaskId);
    }
}
