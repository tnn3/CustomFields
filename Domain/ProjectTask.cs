using System.Collections.Generic;

namespace Domain
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual List<CustomFieldInTasks> CustomFields { get; set; }
    }
}
