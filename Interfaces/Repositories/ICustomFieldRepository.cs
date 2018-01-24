﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Repositories
{
    public interface ICustomFieldRepository : IBaseRepository<CustomField>
    {
        Task<List<CustomField>> AllWithReferencesAsync();
        Task<CustomField> FindWithReferencesAsync(int id);
    }
}
