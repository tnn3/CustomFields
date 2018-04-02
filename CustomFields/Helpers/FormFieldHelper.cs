using System;
using System.Collections.Generic;
using Domain;
using Domain.Enums;
using FormFactory;
using FormFactory.Attributes;

namespace WebApplication.Helpers
{
    public class FormFieldHelper
    {
        public static PropertyVm[] MakeCustomFields<T>(List<CustomField> customFields)
        {
            var propertyvm = new List<PropertyVm>();
            var idCounter = 0;
            foreach (var customField in customFields) {
                var nameStart = typeof(T).Name + "." + nameof(ProjectTask.CustomFields) + "[" + idCounter++ + "].";
                propertyvm.Add(new PropertyVm(typeof(string), nameStart + nameof(CustomFieldInTasks.CustomFieldId)) {
                    IsHidden = true,
                    Value = customField.Id
                });

                var field = new PropertyVm(typeof(string), "")
                {
                    DisplayName = customField.FieldName,
                    NotOptional = customField.IsRequired,
                    Name = nameStart + nameof(CustomFieldInTasks.FieldValue),
                    Id = typeof(T).Name + "_" + nameof(ProjectTask.CustomFields) + "_" + customField.Id + "__" + nameof(CustomFieldInTasks.FieldValue)
                };
                switch (customField.FieldType)
                {
                    case FieldType.Text:
                        field.Type = typeof(string);

                        if (!string.IsNullOrEmpty(customField.RegexPattern))
                        {
                            field.GetCustomAttributes = () => new object[]
                            {
                                new RegularExpressionAttribute(customField.RegexPattern)
                            };
                        }
                        break;
                    case FieldType.Radio:
                        field.Type = typeof(string);
                        field.Choices = customField.PossibleValues.Split(',');
                        field.GetCustomAttributes = () => new object[]
                        {
                            new RadioAttribute()
                        };
                        break;
                    case FieldType.Select:
                        field.Type = typeof(string);
                        field.Choices = customField.PossibleValues.Split(',');
                        break;
                    case FieldType.Checkbox:
                        field.Type = typeof(bool);
                        field.Choices = customField.PossibleValues.Split(',');
                        break;
                    //case FieldType.Date:
                    //    field.Type = typeof(DateTime);
                    //    field.GetCustomAttributes = () => new object[]
                    //    {
                    //        new DateAttribute()
                    //    };
                    //    break;
                    //case FieldType.Datetime:
                    //    field.Type = typeof(DateTime);
                    //    field.GetCustomAttributes = () => new object[]
                    //    {
                    //        new DateTimeAttribute()
                    //    };
                    //    break;
                    case FieldType.Textarea:
                        field.Type = typeof(string);
                        field.GetCustomAttributes = () => new object[]
                        {
                            new MultilineTextAttribute(),
                        };
                        break;
                    default:
                        field.Type = typeof(string);
                        break;
                }
                propertyvm.Add(field);
            }

            return propertyvm.ToArray();
        }
    }
}
