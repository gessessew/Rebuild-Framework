using System.Security.Cryptography;
using System.Text;

namespace Rebuild.Extensions
{
    public static class RSACryptoServiceProviderExtensions
    {
        public static byte[] Decrypt(this RSACryptoServiceProvider provider, byte[] buffer)
        {
            return provider.Decrypt(buffer, false);
        }

        public static string DecryptToString(this RSACryptoServiceProvider provider, byte[] buffer, Encoding encoding = null)
        {
            return provider.Decrypt(buffer).ToString(encoding);
        }

        public static byte[] Encrypt(this RSACryptoServiceProvider provider, byte[] buffer)
        {
            return provider.Encrypt(buffer, false);
        }

        public static byte[] Encrypt(this RSACryptoServiceProvider provider, string s, Encoding encoding = null)
        {
            return provider.Encrypt(s.ToBytes(encoding), false);
        }
    }
}
