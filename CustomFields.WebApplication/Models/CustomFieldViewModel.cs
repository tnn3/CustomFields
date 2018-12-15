using System.Collections.Generic;
using Domain;

namespace WebApplication.Models
{
    public class CustomFieldCreateEditViewModel
    {
        public CustomField2 CustomField2 { get; set; }
        public bool HasExistingData { get; set; }
    }

    public class CustomFieldIndexViewModel
    {
        public IEnumerable<CustomField2> Fields { get; set; }
        public bool ShowHidden { get; set; }
    }
}
