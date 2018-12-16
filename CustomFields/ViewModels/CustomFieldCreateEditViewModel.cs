using CustomFields.Domain;

namespace CustomFields.ViewModels
{
    public class CustomFieldCreateEditViewModel
    {
        public CustomField CustomField { get; set; }
        public bool HasExistingData { get; set; }
    }
}
