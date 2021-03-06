﻿using System.Collections.Generic;
using System.Linq;
using CustomFields.Domain.Enums;
using CustomFields.Interfaces;
using CustomFields.Resources;

namespace CustomFields.Helpers
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

        public static List<string> ValidateCustomField(ICustomField customField, string fieldValue)
        {
            var errors = new List<string>();
            if (customField.IsRequired && string.IsNullOrEmpty(fieldValue))
            {
                errors.Add(string.Format(CustomField.Validation_FieldRequired, customField.FieldName));
            }

            if (string.IsNullOrEmpty(fieldValue)) return errors;

            if (IsOfTextType(customField.FieldType))
            {
                if (customField.MaxLength != null && fieldValue.Length > customField.MaxLength)
                {
                    errors.Add(string.Format(CustomField.Validation_FieldLengthMustBeLowerThan, customField.FieldName, customField.MaxLength));
                }

                if (customField.MinLength != null && fieldValue.Length < customField.MinLength)
                {
                    errors.Add(string.Format(CustomField.Validation_FieldLengthMustBeHigherThan, customField.FieldName, customField.MinLength));
                }

                //if (!string.IsNullOrEmpty(customFielda.RegexPattern) && Regex.IsMatch(vmField.FieldValue, customFielda.RegexPattern))
                //{
                //    errors.Add($"Field {customField.FieldName} must match a pattern of {customField.RegexPattern}");
                //}
            }

            if (IsOfChoosableType(customField.FieldType))
            {
                if (!customField.PossibleValues.Split(',').Any(value => value.Equals(fieldValue)))
                {
                    errors.Add(string.Format(CustomField.Validation_NoSuchSelectedValue, customField.FieldName));
                }
            }

            return errors;
        }

        public static string GetFieldName(ICustomField customField, string culture)
        {
            var fieldTranslation =
                customField.FieldName.FieldNameTranslations?.Find(translation => translation.Locale.Equals(culture))?.Value;

            return fieldTranslation ?? customField.FieldName.FieldDefaultName;
        }
    }
}
