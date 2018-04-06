using Domain.Enums;

namespace WebApplication.Helpers
{
    public static class CustomFieldHelper
    {
        public static bool IsOfChoosableType(FieldType? type)
        {
            return type == FieldType.Checkbox || type == FieldType.Radio || type == FieldType.Select;
        }
    }
}
