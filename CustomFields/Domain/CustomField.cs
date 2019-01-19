using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CustomFields.Domain.Enums;
using CustomFields.Interfaces;

namespace CustomFields.Domain
{
    public class CustomField : ICustomField
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = nameof(FieldType), ResourceType = typeof(Resources.CustomField))]
        public FieldType FieldType { get; set; }
        [MaxLength(100)]
        [Display(Name = nameof(PossibleValues), ResourceType = typeof(Resources.CustomField))]
        public string PossibleValues { get; set; }
        [Display(Name = nameof(MinLength), ResourceType = typeof(Resources.CustomField))]
        public int? MinLength { get; set; }
        [Display(Name = nameof(MaxLength), ResourceType = typeof(Resources.CustomField))]
        public int? MaxLength { get; set; }
        [Display(Name = nameof(IsRequired), ResourceType = typeof(Resources.CustomField))]
        public bool IsRequired { get; set; }
        [Display(Name = nameof(RegexPattern), ResourceType = typeof(Resources.CustomField))]
        public string RegexPattern { get; set; }
        [Display(Name = nameof(Sort), ResourceType = typeof(Resources.CustomField))]
        public int Sort { get; set; }
        [Display(Name = nameof(Status), ResourceType = typeof(Resources.CustomField))]
        public FieldStatus Status { get; set; }

        public int FieldNameId { get; set; }
        [Required]
        public FieldName FieldName { get; set; }

        public List<CustomFieldCombined> CombinedFields { get; set; }
    }
}
