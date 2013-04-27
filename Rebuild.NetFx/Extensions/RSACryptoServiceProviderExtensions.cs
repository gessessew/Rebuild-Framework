using Newtonsoft.Json;
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

        public static string DecryptString(this RSACryptoServiceProvider provider, byte[] buffer, Encoding encoding = null)
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

        public static T JsonDecrypt<T>(this RSACryptoServiceProvider provider, byte[] buffer, Encoding encoding = null, JsonSerializerSettings settings = null)
        {
            return provider
                .DecryptString(buffer, encoding)
                .DeserializeJson<T>(settings);
        }

        public static T JsonDecrypt<T>(this RSACryptoServiceProvider provider, string encryptedString, Encoding encoding = null, JsonSerializerSettings settings = null)
        {
            return provider.JsonDecrypt<T>(encryptedString.ToBase64Bytes(), encoding, settings);
        }

        public static byte[] JsonEncrypt(this RSACryptoServiceProvider provider, object value, Encoding encoding = null, JsonSerializerSettings settings = null, Formatting formating = Formatting.None)
        {
            return provider
                .Encrypt(value
                    .SerializeToJsonString(formating, settings)
                    .ToBytes(encoding)
                );
        }

        public static string JsonEncryptString(this RSACryptoServiceProvider provider, object value, Encoding encoding = null, JsonSerializerSettings settings = null, Formatting formating = Formatting.None)
        {
            return provider
                .JsonEncrypt(value, encoding, settings, formating)
                .ToBase64String();
        }
    }
}
