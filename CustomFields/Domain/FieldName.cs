using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomFields.Domain {
    public class FieldName {
        public int Id { get; set; }
        [MaxLength(30)]
        [Display(Name = nameof(FieldDefaultName), ResourceType = typeof(Resources.CustomField))]
        [Required]
        public string FieldDefaultName { get; set; }
        public List<FieldNameTranslation> FieldNameTranslations { get; set; }
    }
}
