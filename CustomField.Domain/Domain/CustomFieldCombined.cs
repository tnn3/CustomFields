using CustomFields.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomFields.Domain
{
    public class CustomFieldCombined : ICustomFieldCombined
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FieldValue { get; set; }

        [ForeignKey("CustomField")]
        public int CustomFieldId { get; set; }
        public virtual CustomField CustomField { get; set; }
    }
}
