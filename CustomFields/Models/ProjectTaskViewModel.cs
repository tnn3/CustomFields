using Domain;
using FormFactory;

namespace WebApplication.Models
{
    public class ProjectTaskViewModel
    {
        public ProjectTask ProjectTask { get; set; }
        public PropertyVm[] PropertyVms { get; set; }
    }
}
