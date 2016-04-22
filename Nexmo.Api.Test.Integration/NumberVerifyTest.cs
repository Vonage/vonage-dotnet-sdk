using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class NumberVerifyTest
    {
        [Test]
        public void should_control_request()
        {
            var result = NumberVerify.Control(new NumberVerify.ControlRequest
            {
                request_id = "B41F2D19-913C-4BB3-B825-624E375D2C31",
                cmd = "trigger_next_event"
            });
            Assert.AreEqual("19", result.status);
            Assert.AreEqual("trigger_next_event", result.command);
        }
    }
}