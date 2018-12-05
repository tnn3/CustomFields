using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomFields.Domain
{
    public class CustomFieldCombined
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FieldValue { get; set; }

        public int CustomFieldId { get; set; }
        public CustomField CustomField { get; set; }
    }
}
