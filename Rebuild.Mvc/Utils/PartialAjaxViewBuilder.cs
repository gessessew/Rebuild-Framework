using Rebuild.Extensions;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Rebuild.Mvc.Utils
{
    public class PartialAjaxViewBuilder
    {
        private string _actionName;
        private string _controllerName;
        private readonly HtmlHelper _htmlHelper;
        private string _method;
        private string _loadingPartialView;
        private RouteValueDictionary _routeParams;

        internal PartialAjaxViewBuilder(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public PartialAjaxViewBuilder ActionName(string actionName)
        {
            _actionName = actionName;
            return this;
        }

        internal MvcHtmlString Build()
        {
            const string func = "function(data){{$(\"#{0}\")[0].innerHTML = data;}}";
            const string script = "<script>$.ajax({{url:\"{2}\",type:\"{3}\",success:" + func + ",error:" + func + "}});</script>";

            var s = string.Format(
                "<div id=\"{0}\">{1}</div>" + script,
                CreateDivId(),
                RenderLoadingView(),
                CreateUrl(),
                _method.IfNoValue("GET")
            );

            return new MvcHtmlString(s);
        }

        public PartialAjaxViewBuilder ControllerName(string controllerName)
        {
            _controllerName = controllerName;
            return this;
        }

        private static string CreateDivId()
        {
            return "D" + Guid.NewGuid().ToString("D").Replace("-", "");
        }

        private string CreateUrl()
        {
            return UrlHelper
                .GenerateUrl(
                    null,
                    _actionName,
                    _controllerName,
                    null,
                    null,
                    null,
                    _routeParams,
                    _htmlHelper.RouteCollection,
                    _htmlHelper.ViewContext.RequestContext,
                    true
                );
        }

        public PartialAjaxViewBuilder LoadingPartialView(string loadingPartialView)
        {
            _loadingPartialView = loadingPartialView;
            return this;
        }

        public PartialAjaxViewBuilder Method(string method)
        {
            _method = method;
            return this;
        }

        private MvcHtmlString RenderLoadingView()
        {
            return _loadingPartialView.HasValue() ? _htmlHelper.Partial(_loadingPartialView) : null;
        }

        public PartialAjaxViewBuilder RouteParams(object routeParams)
        {
            _routeParams = routeParams == null ? null : new RouteValueDictionary(routeParams);
            return this;
        }

        public PartialAjaxViewBuilder RouteParams(RouteValueDictionary routeParams)
        {
            _routeParams = routeParams;
            return this;
        }
    }
}