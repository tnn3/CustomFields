using CustomFields.Domain;

namespace Domain
{
    public class CustomField2 : CustomField
    {
        public CustomField2()
        {
        }

        public CustomField2(CustomField customField)
        {
            Id = customField.Id;
            FieldName = customField.FieldName;
            FieldType = customField.FieldType;
            PossibleValues = customField.PossibleValues;
            MinLength = customField.MinLength;
            MaxLength = customField.MaxLength;
            IsRequired = customField.IsRequired;
            Sort = customField.Sort;
            Status = customField.Status;
            CombinedFields = customField.CombinedFields;
        }
    }
}
