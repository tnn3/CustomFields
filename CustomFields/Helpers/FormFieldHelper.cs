using System.Collections.Generic;
using CustomFields.Domain;
using CustomFields.Domain.Enums;
using CustomFields.Interfaces;
using FormFactory;
using FormFactory.Attributes;

namespace CustomFields.Helpers
{
    public class FormFieldHelper
    {
        public static PropertyVm[] MakeCustomFields<T>(List<ICustomField> customFields, bool enableReadonly, string fieldNameReferencingCustomFields, int? formId = null)
        {
            var propertyvm = new List<PropertyVm>();
            var idCounter = 0;

            foreach (var customField in customFields) {
                bool editing = formId != null && customField.CombinedFields != null;

                var nameStart = $"{ typeof(T).Name }.{ fieldNameReferencingCustomFields }[{ idCounter++}].";
                propertyvm.Add(new PropertyVm(typeof(string), nameStart + nameof(CustomFieldCombined.CustomFieldId)) {
                    IsHidden = true,
                    Value = customField.Id
                });

                var field = new PropertyVm(typeof(string), "")
                {
                    DisplayName = customField.FieldName,
                    NotOptional = customField.IsRequired,
                    Name = nameStart + nameof(CustomFieldCombined.FieldValue),
                    Id = typeof(T).Name + "_" + fieldNameReferencingCustomFields + "_" + customField.Id + "__" + nameof(CustomFieldCombined.FieldValue),
                    Readonly = enableReadonly && customField.Status == FieldStatus.Disabled
                };

                if (editing)
                {
                    field.Value = customField.CombinedFields[0].FieldValue;
                }

                switch (customField.FieldType)
                {
                    case FieldType.Text:
                        if (!string.IsNullOrEmpty(customField.RegexPattern))
                        {
                            field.GetCustomAttributes = () => new object[]
                            {
                                new RegularExpressionAttribute(customField.RegexPattern)
                            };
                        }
                        break;
                    case FieldType.Radio:
                        field.Choices = customField.PossibleValues.Split(',');
                        field.GetCustomAttributes = () => new object[]
                        {
                            new RadioAttribute()
                        };
                        break;
                    case FieldType.Select:
                        field.Choices = customField.PossibleValues.Split(',');
                        break;
                    case FieldType.Checkbox:
                        field.Type = typeof(bool);
                        break;
                    case FieldType.Textarea:
                        field.GetCustomAttributes = () => new object[]
                        {
                            new MultilineTextAttribute()
                        };
                        break;
                }
                propertyvm.Add(field);
            }

            return propertyvm.ToArray();
        }
    }
}
