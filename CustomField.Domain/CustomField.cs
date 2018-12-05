using System.ComponentModel.DataAnnotations;
using CustomFields.Domain.Enums;

namespace CustomFields.Domain
{
    public class CustomField
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Field name")]
        public string FieldName { get; set; }
        [Required]
        [Display(Name = "Field type")]
        public FieldType? FieldType { get; set; }
        [MaxLength(100)]
        [Display(Name = "Values to choose from")]
        public string PossibleValues { get; set; }
        [Display(Name = "Minimum length of field value")]
        public int? MinLength { get; set; }
        [Display(Name = "Maximum length of field value")]
        public int? MaxLength { get; set; }
        [Display(Name = "Field value is required")]
        public bool IsRequired { get; set; }
        [Display(Name = "Validation pattern")]
        public string RegexPattern { get; set; }
        public int Sort { get; set; }
        public FieldStatus Status { get; set; }
    }
}
