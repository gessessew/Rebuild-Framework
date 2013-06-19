using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Rebuild.Extensions;

namespace Rebuild.Mvc.Utils
{
    public class XslTransformBuilder
    {
        private readonly XsltArgumentList _argumentList;
        private bool _enableDebug;
        private readonly HtmlHelper _htmlHelper;
        private XmlNode _nodeInput;
        private XmlNode _nodeStyleSheet;
        private XmlReader _readerInput;
        private XmlReader _readerStyleSheet;

        internal XslTransformBuilder(HtmlHelper htmlHelper)
        {
            _argumentList = new XsltArgumentList();
            _htmlHelper = htmlHelper;
        }

        public XslTransformBuilder AddExtensionObject(string namespaceUri, object extension)
        {
            _argumentList.AddExtensionObject(namespaceUri, extension);
            return this;
        }

        public XslTransformBuilder AddExtensionObject(XNamespace ns, object extension)
        {
            _argumentList.AddExtensionObject(ns.NamespaceName, extension);
            return this;
        }

        public XslTransformBuilder AddParam(string name, string namespaceUri, object parameter)
        {
            _argumentList.AddParam(name, namespaceUri, parameter);
            return this;
        }

        public XslTransformBuilder AddParam(XName name, object parameter)
        {
            _argumentList.AddParam(name.LocalName, name.NamespaceName, parameter);
            return this;
        }

        internal MvcHtmlString Build()
        {
            using (_readerInput)
            using (_readerStyleSheet)
            {
                var transform = new XslCompiledTransform(_enableDebug);

                if (_nodeStyleSheet != null)
                {
                    transform.Load(_nodeStyleSheet);
                }
                else if (_readerStyleSheet != null)
                {
                    transform.Load(_readerStyleSheet);
                }

                var sw = new StringWriter();

                if (_nodeInput != null)
                {
                    transform.Transform(_nodeInput, _argumentList, sw);
                }
                else if (_readerInput != null)
                {
                    transform.Transform(_readerInput, _argumentList, sw);
                }

                return new MvcHtmlString(sw.ToString());
            }
        }

        public XslTransformBuilder EnableDebug(bool enableDebug)
        {
            _enableDebug = enableDebug;
            return this;
        }

        public XslTransformBuilder Input(string fileName)
        {
            var mappedFilename = _htmlHelper.ViewContext.RequestContext.HttpContext.Server.MapPath(fileName);
            return this.Input(new FileStream(mappedFilename, FileMode.Create, FileAccess.Read));
        }

        public XslTransformBuilder Input(Stream stream)
        {
            return this.Input(XmlReader.Create(stream));
        }

        public XslTransformBuilder Input(XmlReader reader)
        {
            _nodeInput = null;
            _readerInput.DisposeIfNotNull();
            _readerInput = reader;
            return this;
        }

        public XslTransformBuilder Input(XmlNode node)
        {
            _nodeInput = node;
            _readerInput.DisposeIfNotNull();
            _readerInput = null;
            return this;
        }

        public XslTransformBuilder StyleSheet(string fileName)
        {
            var mappedFilename = _htmlHelper.ViewContext.RequestContext.HttpContext.Server.MapPath(fileName);
            return this.StyleSheet(new FileStream(mappedFilename, FileMode.Create, FileAccess.Read));
        }

        public XslTransformBuilder StyleSheet(Stream stream)
        {
            return this.StyleSheet(XmlReader.Create(stream));
        }

        public XslTransformBuilder StyleSheet(XmlReader reader)
        {
            _nodeStyleSheet = null;
            _readerStyleSheet.DisposeIfNotNull();
            _readerStyleSheet = reader;
            return this;
        }

        public XslTransformBuilder StyleSheet(XNode node)
        {
            return this.StyleSheet(node.ToXmlDocument());
        }

        public XslTransformBuilder StyleSheet(XmlNode node)
        {
            _nodeStyleSheet = node;
            _readerStyleSheet.DisposeIfNotNull();
            _readerStyleSheet = null;
            return this;
        }
    }
}
