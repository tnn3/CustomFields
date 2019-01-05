using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using CustomFields.Interfaces;
using CustomFields.ViewModels;

namespace CustomFields
{
    public static class RenderExtensions
    {
        public static IHtmlContent Render(this IEnumerable<ICustomField> properties, string mainClassName, string customFieldReferenceName, 
            IHtmlHelper html, int? mainClassId = null)
        {
            var list = properties.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                string start = $"{mainClassName}.{customFieldReferenceName}[{i}]";
                stringBuilder.AppendLine(list[i].GetFieldPartialView(html, start).ToHtmlString());
            }

            return new HtmlString(stringBuilder.ToString());
        }

        private static IHtmlContent GetFieldPartialView(this ICustomField propertyVm, IHtmlHelper html, string start)
        {
            string partialViewName;
            switch (propertyVm.FieldType)
            {
                case Domain.Enums.FieldType.Text:
                    partialViewName = "CustomField/FieldType.Text";
                    break;
                case Domain.Enums.FieldType.Radio:
                    partialViewName = "CustomField/FieldType.Radio";
                    break;
                case Domain.Enums.FieldType.Checkbox:
                    partialViewName = "CustomField/FieldType.Checkbox";
                    break;
                case Domain.Enums.FieldType.Select:
                    partialViewName = "CustomField/FieldType.Select";
                    break;
                case Domain.Enums.FieldType.Textarea:
                    partialViewName = "CustomField/FieldType.Textarea";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            //TODO move to partial async
            return html.Partial(partialViewName, new FormFieldViewModel
            {
                CustomField = propertyVm,
                Start = start
            }, null);
        }

        private static string ToHtmlString(this IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
