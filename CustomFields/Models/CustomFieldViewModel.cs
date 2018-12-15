using System.Collections.Generic;
using CustomFields.Domain;

namespace WebApplication.Models
{
    public class CustomFieldCreateEditViewModel
    {
        public CustomField CustomField2 { get; set; }
        public bool HasExistingData { get; set; }
    }

    public class CustomFieldIndexViewModel
    {
        public IEnumerable<CustomField> Fields { get; set; }
        public bool ShowHidden { get; set; }
    }
}
