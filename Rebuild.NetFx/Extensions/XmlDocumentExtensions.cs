using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Rebuild.Extensions
{
    public static class XmlDocumentExtensions
    {
        private static XmlNodeList GetSignatures(this XmlDocument document)
        {
            return document.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl);
        }

        public static bool IsSignatureValid(this XmlDocument document, X509Certificate2 certificate)
        {
            return document.IsSignatureValid(certificate.PublicKeyRsa());
        }

        public static bool IsSignatureValid(this XmlDocument document, RSA publicKey)
        {
            var signedXml = new SignedXml(document);
            var nodeList = document.GetSignatures();

            if (nodeList.Count == 0)
            {
                throw new CryptographicException("No Signature was found in the document.");
            }

            if (nodeList.Count != 1)
            {
                throw new CryptographicException("More that one signature was found for the document.");
            }

            signedXml.LoadXml((XmlElement)nodeList[0]);

            return signedXml.CheckSignature(publicKey);
        }

        public static XmlDocument RemoveSignatures(this XmlDocument document)
        {
            foreach(XmlNode node in document.GetSignatures())
            {
                var parent = node.ParentNode;

                if (parent != null)
                {
                    parent.RemoveChild(node);
                }
            }

            return document;
        }

        public static XmlDocument Sign(this XmlDocument document, X509Certificate2 certificate)
        {
            return document.Sign(certificate.PrivateKeyRsa());
        }

        public static XmlDocument Sign(this XmlDocument document, RSA privateKey)
        {
            var signedXml = new SignedXml(document);
            signedXml.SigningKey = privateKey;

            var reference = new Reference { Uri = "" };
            var env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            var xmlDigitalSignature = signedXml.GetXml();

            document
                .DocumentElement
                .AppendChild(document.ImportNode(xmlDigitalSignature, true));

            return document;
        }
    }
}
