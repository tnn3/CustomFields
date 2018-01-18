using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain
{
    public class CustomField
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Field name")]
        public string FieldName { get; set; }
        public FieldType FieldType { get; set; }
        [MaxLength(100)]
        [Display(Name = "Possible values")]
        public string PossibleValues { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public bool IsRequired { get; set; }

        public virtual List<CustomFieldInTasks> Tasks { get; set; }
    }
}
