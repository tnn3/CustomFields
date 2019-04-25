using CustomFields.Interfaces;

namespace CustomFields.ViewModels
{
    public class FormFieldViewModel
    {
        public ICustomField CustomField { get; set; }
        public string Start { get; set; }
        public string SelectedValue => CustomField.CombinedFields != null ? CustomField.CombinedFields[0].FieldValue : string.Empty;
    }
}
