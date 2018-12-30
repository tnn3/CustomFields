using System.Collections.Generic;
using CustomFields.Interfaces;
using Domain;

namespace WebApplication.Models
{
    public class ProjectTaskViewModel
    {
        public ProjectTask ProjectTask { get; set; }
        public IEnumerable<ICustomField> CustomFields { get; set; }
    }
}
