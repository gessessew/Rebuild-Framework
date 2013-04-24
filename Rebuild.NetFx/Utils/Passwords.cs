using System;
using System.Security.Cryptography;

namespace Rebuild.Utils
{
    #region struct HashedPassword
    public struct HashedPassword
    {
        internal HashedPassword(string password, string salt)
            : this()
        {
            Password = password;
            Salt = salt;
        }

        public string Password { get; private set; }
        public string Salt { get; private set; }
    }
    #endregion

    public static class Passwords
    {
        private static bool Equals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GenerateSalt(int saltLength = 24)
        {
            var salt = new byte[saltLength];
            new RNGCryptoServiceProvider().GetBytes(salt);
            return salt;
        }

        private static byte[] Hash(string password, byte[] salt, int iterations = 1000, int outputLength = 24)
        {
            return new Rfc2898DeriveBytes(password, salt, iterations).GetBytes(outputLength);
        }

        public static HashedPassword HashPassword(string password)
        {
            var salt = GenerateSalt();
            return new HashedPassword(Convert.ToBase64String(Hash(password, salt)), Convert.ToBase64String(salt));
        }

        public static bool ValidatePassword(string password, string passwordHash, string salt)
        {
            return Equals(Convert.FromBase64String(passwordHash), Hash(password, Convert.FromBase64String(salt)));
        }
    }
}
