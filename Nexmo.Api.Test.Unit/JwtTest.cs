using System.Configuration;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    public class JwtTest
    {
        [Test]
        public void should_generate_jwt()
        {
            var tok = Jwt.CreateToken(ConfigurationManager.AppSettings["Nexmo.Application.Id"], ConfigurationManager.AppSettings["Nexmo.Application.Key"]);
            Assert.IsFalse(string.IsNullOrEmpty(tok));
        }
    }
}