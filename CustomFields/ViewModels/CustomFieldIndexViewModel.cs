using System.Collections.Generic;
using CustomFields.Domain;

namespace CustomFields.ViewModels
{
    public class CustomFieldIndexViewModel
    {
        public IEnumerable<CustomField> Fields { get; set; }
        public bool ShowHidden { get; set; }
    }
}