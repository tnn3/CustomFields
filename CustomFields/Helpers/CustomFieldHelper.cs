using Domain.Enums;

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
    }
}
