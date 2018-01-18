using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Interfaces;
using Interfaces.Repositories;

namespace DAL.Repositories
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbContext dataContext) : base(dataContext)
        {
        }
    }
}
