using System;
using System.Collections.Generic;
using System.Text;
using CustomFields.Domain;

namespace CustomFields.Interfaces.Repositories
{
    public interface ICustomFieldCombinedRepository
    {
        IEnumerable<CustomFieldCombined> All();
        CustomFieldCombined Find(params object[] id);
    }
}
