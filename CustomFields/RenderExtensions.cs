using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using CustomFields.Helpers;
using CustomFields.Interfaces;
using FormFactory.AspMvc;

namespace CustomFields
{
    public static class RenderExtensions
    {
        public static IHtmlContent Render(this IEnumerable<ICustomField> properties, string mainClassName, string customFieldReferenceName, 
            IHtmlHelper html, int? mainClassId = null)
        {
            var propertyVms = FormFieldHelper.MakeCustomFields(properties.ToList(), mainClassName, customFieldReferenceName, mainClassId);
            return propertyVms.Render(html);
        }
    }
}
