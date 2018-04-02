using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class CustomFieldInTasks
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FieldValue { get; set; }

        public int CustomFieldId { get; set; }
        public CustomField CustomField { get; set; }

        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
