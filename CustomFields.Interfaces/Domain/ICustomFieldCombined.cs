using System;
using System.Collections.Generic;
using System.Text;
using CustomFields.Domain;

namespace CustomFields.Interfaces.Domain
{
    public interface ICustomFieldCombined
    {
        int Id { get; set; }

        string FieldValue { get; set; }

        int CustomFieldId { get; set; }
        CustomField CustomField { get; set; }
    }
}
