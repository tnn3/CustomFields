using System.Collections.Generic;
using CustomFields.Interfaces;
using Domain;
using WebApplication.Helpers;

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
