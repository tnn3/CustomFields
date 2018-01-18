using Domain;
using Interfaces;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class ProjectTaskRepository : BaseRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(IDbContext dataContext) : base(dataContext)
        {
        }
    }
}
