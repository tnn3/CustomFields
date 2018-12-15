using CustomFields.Domain;

namespace CustomFields.ViewModels
{
    public class CustomFieldCreateEditViewModel<T> where T : CustomField
    {
        public T CustomField2 { get; set; }
        public bool HasExistingData { get; set; }
    }
}
