using CustomFields.Domain;

namespace CustomFields.Interfaces
{
    public interface ICustomFieldCombined
    {
        int Id { get; set; }

        string FieldValue { get; set; }

        int CustomFieldId { get; set; }
        CustomField CustomField { get; set; }
    }
}
