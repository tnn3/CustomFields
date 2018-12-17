using System.Collections.Generic;
using CustomFields.Domain;

namespace CustomFields.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CustomField> Fields { get; set; }
        public bool ShowHidden { get; set; }
    }
}