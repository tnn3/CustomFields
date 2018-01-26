using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Repositories
{
    public interface IProjectTaskRepository : IBaseRepository<ProjectTask>
    {
        Task<List<ProjectTask>> AllAsyncWithReferences();
        Task<ProjectTask> FindAsyncWithReferences(int id);
    }
}
