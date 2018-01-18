using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Repositories;
using Domain;
using Interfaces;

namespace DAL.Repositories
{
    public class CustomFieldRepository : BaseRepository<CustomField>, ICustomFieldRepository
    {
        public CustomFieldRepository(IDbContext dataContext) : base(dataContext)
        {
        }
    }
}
