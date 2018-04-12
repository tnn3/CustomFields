using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Enums;
using FormFactory;
using FormFactory.Attributes;

namespace WebApplication.Helpers
{
    public class FormFieldHelper
    {
        public static PropertyVm[] MakeCustomFields<T>(List<CustomField> customFields, bool enableReadonly, int? formId = null)
        {
            var propertyvm = new List<PropertyVm>();
            var idCounter = 0;

            foreach (var customField in customFields) {
                bool editing = formId != null && customField.Tasks != null;

                var nameStart = $"{ typeof(T).Name }.{ nameof(ProjectTask.CustomFields) }[{ idCounter++}].";
                propertyvm.Add(new PropertyVm(typeof(string), nameStart + nameof(CustomFieldInTasks.CustomFieldId)) {
                    IsHidden = true,
                    Value = customField.Id
                });

                var field = new PropertyVm(typeof(string), "")
                {
                    DisplayName = customField.FieldName,
                    NotOptional = customField.IsRequired,
                    Name = nameStart + nameof(CustomFieldInTasks.FieldValue),
                    Id = typeof(T).Name + "_" + nameof(ProjectTask.CustomFields) + "_" + customField.Id + "__" + nameof(CustomFieldInTasks.FieldValue),
                    Readonly = enableReadonly && customField.Status == FieldStatus.Disabled
                };

                List<CustomFieldInTasks> formFields = new List<CustomFieldInTasks>();

                if (editing)
                {
                    formFields = customField.Tasks.Where(f => f.ProjectTaskId == formId).ToList();
                }

                if (customField.FieldType is FieldType.Text || customField.FieldType is FieldType.Textarea ||
                    customField.FieldType is FieldType.Select || customField.FieldType is FieldType.Radio)
                {
                    field.Type = typeof(string);
                    if (formFields.Any())
                    {
                        field.Value = formFields.First().FieldValue;
                    }
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
                        field.Choices = customField.PossibleValues.Split(',');
                        break;
                    case FieldType.Textarea:
                        field.GetCustomAttributes = () => new object[]
                        {
                            new MultilineTextAttribute()
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
