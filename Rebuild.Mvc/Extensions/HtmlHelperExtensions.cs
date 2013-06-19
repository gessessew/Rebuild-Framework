using Rebuild.Mvc.Utils;
using System;
using System.Web.Mvc;

namespace Rebuild.Mvc.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString PartialAjaxView(this HtmlHelper htmlHelper, Func<PartialAjaxViewBuilder, PartialAjaxViewBuilder> builder)
        {
            return builder(new PartialAjaxViewBuilder(htmlHelper)).Build();
        }

        public static MvcHtmlString XslTransform(this HtmlHelper htmlHelper, Func<XslTransformBuilder, XslTransformBuilder> builder)
        {
            return builder(new XslTransformBuilder(htmlHelper)).Build();
        }
    }
}
