using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class NumberVerifyTest : MockedWebTest
    {
        [Test]
        public void should_send_control()
        {
            SetExpect($"{ApiUrl}/verify/control/json?request_id=B41F2D19-913C-4BB3-B825-624E375D2C31&cmd=cancel&api_key={ApiKey}&api_secret={ApiSecret}&",
"{\"status\":\"0\",\"command\":\"cancel\"}");

            var results = NumberVerify.Control(new NumberVerify.ControlRequest
            {
                request_id = "B41F2D19-913C-4BB3-B825-624E375D2C31",
                cmd = "cancel"
            });

            Assert.AreEqual("0", results.status);
            Assert.AreEqual("cancel", results.command);
        }
    }
}