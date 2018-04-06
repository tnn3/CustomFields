using Domain;
using WebApplication.Helpers;

namespace WebApplication.Models
{
    public class CustomFieldViewModel
    {
        public CustomField CustomField { get; set; }
        public bool HasExistingData { get; set; }
    }
}
