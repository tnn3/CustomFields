using System;
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
        public static PropertyVm[] MakeCustomFields(List<ICustomField> customFields, string mainClassName, string customFieldReferenceName, int? mainClassId = null)
        {
            var propertyVms = new List<PropertyVm>();
            var idCounter = 0;

            foreach (var customField in customFields) {
                bool editing = mainClassId != null && customField.CombinedFields != null;

                string nameStart = $"{ mainClassName }.{ customFieldReferenceName }[{ idCounter++ }].";
                propertyVms.Add(new PropertyVm(typeof(string), nameStart + nameof(CustomFieldCombined.CustomFieldId)) {
                    IsHidden = true,
                    Value = customField.Id
                });

                var field = new PropertyVm(typeof(string), "")
                {
                    DisplayName = customField.FieldName,
                    NotOptional = customField.IsRequired,
                    Name = nameStart + nameof(CustomFieldCombined.FieldValue),
                    Id = mainClassName + "_" + customFieldReferenceName + "_" + customField.Id + "__" + nameof(CustomFieldCombined.FieldValue),
                    Readonly = editing && customField.Status == FieldStatus.Disabled
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
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                propertyVms.Add(field);
            }

            return propertyVms.ToArray();
        }
    }
}
