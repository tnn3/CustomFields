using System.ComponentModel.DataAnnotations.Schema;
using CustomFields.Domain;

namespace Domain
{
    public class CustomFieldInTasks : CustomFields.Domain.CustomFieldCombined
    {
        //public int Id { get; set; }
        //public string FieldValue { get; set; }
        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
        //public int CustomFieldId { get; set; }
        //public CustomFields.Domain.CustomField2 CustomField2 { get; set; }
    }
}
