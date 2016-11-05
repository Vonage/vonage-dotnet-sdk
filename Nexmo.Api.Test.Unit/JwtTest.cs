using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    public class JwtTest
    {
        [Test]
        public void should_generate_jwt()
        {
            var tok = Jwt.CreateToken(Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"], Configuration.Instance.Settings["appSettings:Nexmo.Application.Key"]);
            Assert.IsFalse(string.IsNullOrEmpty(tok));
        }
    }
}