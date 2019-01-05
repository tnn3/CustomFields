using CustomFields.Interfaces;

namespace CustomFields.ViewModels
{
    public class FormFieldViewModel
    {
        public ICustomField CustomField { get; set; }
        public string Start { get; set; }
    }
}
