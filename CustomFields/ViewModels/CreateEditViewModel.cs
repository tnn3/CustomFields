using CustomFields.Interfaces;

namespace CustomFields.ViewModels
{
    public class CreateEditViewModel<T> where T : ICustomField
    {
        public virtual T CustomField { get; set; }
        public bool HasExistingData { get; set; }
    }
}
