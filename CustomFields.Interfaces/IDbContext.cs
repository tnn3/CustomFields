using CustomFields.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomFields.Interfaces
{
    public interface IDbContext
    {
        DbSet<CustomField> CustomFields { get; set; }
        DbSet<CustomFieldCombined> CustomFieldCombined { get; set; }
    }
}
