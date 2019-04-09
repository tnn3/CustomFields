using System.ComponentModel.DataAnnotations;

namespace CustomFields.Domain.Enums
{
    public enum FieldStatus
    {
        [Display(Name = "FieldStatusHidden", ResourceType = typeof(Resources.CustomField))]
        Hidden, //field will not be shown in create/edit
        [Display(Name = "FieldStatusActive", ResourceType = typeof(Resources.CustomField))]
        Active
    }
}
