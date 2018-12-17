using CustomFields.Domain;

namespace Domain
{
    public class CustomFieldInTasks : CustomFieldCombined
    {
        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
