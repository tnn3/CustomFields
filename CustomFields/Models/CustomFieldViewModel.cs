using System.Collections.Generic;
using Domain;
using WebApplication.Helpers;

namespace WebApplication.Models
{
    public class CustomFieldCreateEditViewModel
    {
        public CustomField CustomField { get; set; }
        public bool HasExistingData { get; set; }
    }

    public class CustomFieldIndexViewModel
    {
        public IEnumerable<CustomField> Fields { get; set; }
        public bool ShowHidden { get; set; }
    }
}
