using CustomFields.Domain;

namespace CustomFields.ViewModels
{
    public class CreateEditViewModel
    {
        public virtual CustomField CustomField { get; set; }
        public bool HasExistingData { get; set; }
    }
}
