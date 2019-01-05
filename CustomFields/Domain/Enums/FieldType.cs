using System.ComponentModel.DataAnnotations;

namespace CustomFields.Domain.Enums
{
    public enum FieldType
    {
        [Display(Name = "FieldTypeText", ResourceType = typeof(Resources.CustomField))]
        Text,
        [Display(Name = "FieldTypeRadio", ResourceType = typeof(Resources.CustomField))]
        Radio,
        [Display(Name = "FieldTypeCheckbox", ResourceType = typeof(Resources.CustomField))]
        Checkbox,
        [Display(Name = "FieldTypeSelect", ResourceType = typeof(Resources.CustomField))]
        Select,
        [Display(Name = "FieldTypeTextarea", ResourceType = typeof(Resources.CustomField))]
        Textarea,
        /*Date,
        Datetime,
        Email,
        Number,
        Range,
        Time,
        Url,
        Month,
        TextWithSuggestion*/
    }
}
