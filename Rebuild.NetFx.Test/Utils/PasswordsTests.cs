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
            Passwords
                .HashPassword("123456")
                .Do(hash => Assert.AreNotEqual("123456", hash.Password))
                .Do(hash => string.IsNullOrWhiteSpace(hash.Password).AssertEqual(false))
                .Do(hash => string.IsNullOrWhiteSpace(hash.Salt).AssertEqual(false))
                .Do(hash => Passwords.ValidatePassword("123456", hash.Password, hash.Salt).AssertEqual(true))
                .Do(hash => Passwords.ValidatePassword("123457", hash.Password, hash.Salt).AssertEqual(false));
        }
    }
}
