using JWTAuthority.Helpers;
using NUnit.Framework;
using System.Security.Cryptography;

namespace JWTAuthority.Tests
{
    [TestFixture]
    class HashHelperTests
    {
        private HashHelper _hashHelper;

        [SetUp]
        public void Setup()
        {
            _hashHelper = new HashHelper(new SHA256CryptoServiceProvider());
        }

        [TestCase("test", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08")]
        [TestCase("test1234", "937e8d5fbb48bd4949536cd65b8d35c426b80d2f830c5c308e2cdec422ae2244")]
        [TestCase("26123123112alex", "38db8d3817e101015a41282034d74ced64030c76ae7484631ada1b6734dd6d9e")]
        public void GetHashAsString_WhenCalled_CorrectHashIsReturned(string message, string expectedhash)
        {
            //Arrange
            //Act
            var hashedMessage = _hashHelper.GetHashAsString(message);

            //Assert
            Assert.AreEqual(expectedhash, hashedMessage);
        }
    }
}
