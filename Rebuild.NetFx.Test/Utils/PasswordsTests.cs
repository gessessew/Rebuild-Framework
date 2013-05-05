using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rebuild.Extensions;

namespace Rebuild.Utils
{
    [TestClass]
    public class PasswordsTests
    {
        [TestMethod]
        public void HashAndValidatePassword()
        {
            var hash = Passwords.HashPassword("123456");

            Assert.AreNotEqual("123456", hash.Password);
            string.IsNullOrWhiteSpace(hash.Password).AssertEqual(false);
            string.IsNullOrWhiteSpace(hash.Salt).AssertEqual(false);
            Passwords.ValidatePassword("123456", hash.Password, hash.Salt).AssertEqual(true);
            Passwords.ValidatePassword("123457", hash.Password, hash.Salt).AssertEqual(false);
        }
    }
}
