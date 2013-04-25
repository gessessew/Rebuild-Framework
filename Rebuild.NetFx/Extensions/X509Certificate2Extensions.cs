using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Rebuild.Extensions
{
    public static class X509Certificate2Extensions
    {
        public static RSACryptoServiceProvider PrivateKeyRsaProvider(this X509Certificate2 certificate)
        {
            return (RSACryptoServiceProvider)certificate.PrivateKey;
        }

        public static RSACryptoServiceProvider PublicKeyRsaProvider(this X509Certificate2 certificate)
        {
            return (RSACryptoServiceProvider)certificate.PublicKey.Key;
        }
    }
}
