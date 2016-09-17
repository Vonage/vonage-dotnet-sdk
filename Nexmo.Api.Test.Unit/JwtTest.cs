using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    public class JwtTest
    {
        [Test]
        public void should_generate_jwt()
        {
            // TODO
            //var execDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //var privateKeyFile = Path.Combine(execDir, "private.key");

            var privateKeyFile = @"C:\path\to\your\application\private.key";
            var tok = Jwt.CreateToken("ffffffff-ffff-ffff-ffff-ffffffffffff", privateKeyFile);
            Assert.IsFalse(string.IsNullOrEmpty(tok));
        }
    }
}