using System.Collections.Generic;
using System.Linq;
using CustomFields.Domain;
using CustomFields.Domain.Enums;

namespace WebApplication.Helpers
{
    public static class CustomFieldHelper
    {
        public static bool IsOfChoosableType(FieldType? type)
        {
            return type == FieldType.Radio || type == FieldType.Select;
        }

        public static bool IsOfTextType(FieldType? type)
        {
            return type == FieldType.Text || type == FieldType.Textarea;
        }

        public static List<string> ValidateCustomField(CustomField customField, string fieldValue)
        {
            var errors = new List<string>();

            if (customField.IsRequired && string.IsNullOrEmpty(fieldValue))
            {
                errors.Add($"Field {customField.FieldName} is required");
            }

            if (string.IsNullOrEmpty(fieldValue)) return errors;

            if (CustomFieldHelper.IsOfTextType(customField.FieldType))
            {
                if (customField.MaxLength != null && fieldValue.Length > customField.MaxLength)
                {
                    errors.Add($"Field {customField.FieldName} length must be lower than {customField.MaxLength}");
                }

                if (customField.MinLength != null && fieldValue.Length < customField.MinLength)
                {
                    errors.Add($"Field {customField.FieldName} length must be higher than {customField.MinLength}");
                }

                //if (!string.IsNullOrEmpty(customFielda.RegexPattern) && Regex.IsMatch(vmField.FieldValue, customFielda.RegexPattern))
                //{
                //    errors.Add($"Field {customField.FieldName} must match a pattern of {customField.RegexPattern}");
                //}
            }

            if (CustomFieldHelper.IsOfChoosableType(customField.FieldType))
            {
                if (!customField.PossibleValues.Split(',').Any(value => value.Equals(fieldValue)))
                {
                    errors.Add($"Field {customField.FieldName} does not contain selected value");
                }
            }

            return errors;
        }
    }
}
