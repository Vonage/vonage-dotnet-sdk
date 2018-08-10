using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nexmo.Api.Test.Unit
{
    [TestClass]
    public class JwtTest
    {
        [TestMethod]
        public void should_generate_jwt()
        {
            var tok = Jwt.Generate(Configuration.Instance.Settings["appSettings:Nexmo.Application.Id"], Configuration.Instance.Settings["appSettings:Nexmo.Application.Key"]);
            Assert.IsFalse(string.IsNullOrEmpty(tok));
        }
    }
}