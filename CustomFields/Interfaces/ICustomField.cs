using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomFields.Domain;
using CustomFields.Domain.Enums;

namespace CustomFields.Interfaces
{
    public interface ICustomField
    {
        int Id { get; set; }
        [Required]
        [MaxLength(30)]
        string FieldName { get; set; }
        [Required]
        FieldType? FieldType { get; set; }
        [MaxLength(100)]
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
