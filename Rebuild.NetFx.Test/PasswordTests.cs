using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rebuild.Utils;
using Rebuild.Extensions;

namespace Rebuild.NetFx
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void Hash()
        {
            const string password = "123";
            var salt = Passwords.GenerateSalt();
            var hash = Passwords.Hash(password, salt);

            Passwords.Hash(password, salt).AssertEqual(hash);
            Assert.AreNotEqual(password, hash);
            Assert.AreNotEqual(salt, hash);

            hash = Passwords.Hash("", salt);

            Assert.IsTrue(hash.Length > 0);
            Passwords.Hash(null, salt).AssertEqual(hash);
        }

        [TestMethod]
        public void Validate()
        {
            var salt = Passwords.GenerateSalt();
            Assert.IsTrue(Passwords.Validate(Passwords.Hash("123", salt), "123", salt));
            Assert.IsFalse(Passwords.Validate(Passwords.Hash("a", salt), "A", salt));
            Assert.IsTrue(Passwords.Validate(Passwords.Hash("", salt), null, salt));
        }
    }
}
