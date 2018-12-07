using System.Collections.Generic;
using CustomFields.Domain;
using CustomFields.Domain.Enums;

namespace CustomFields.Interfaces
{
    public interface ICustomField
    {
        int Id { get; set; }
        string FieldName { get; set; }
        FieldType? FieldType { get; set; }
        string PossibleValues { get; set; }
        int? MinLength { get; set; }
        int? MaxLength { get; set; }
        bool IsRequired { get; set; }
        string RegexPattern { get; set; }
        int Sort { get; set; }
        FieldStatus Status { get; set; }

        List<CustomFieldCombined> CombinedFields { get; set; }
    }
}
