using System;
using System.IO;
using System.Text;
using Moq;
using Nexmo.Api.Request;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class NumberVerifyTest : MockedWebTest
    {
        [Test]
        public void should_send_control()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"status\":\"0\",\"command\":\"cancel\"}")));
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var results = NumberVerify.Control(new NumberVerify.ControlRequest
            {
                request_id = "B41F2D19-913C-4BB3-B825-624E375D2C31",
                cmd = "cancel"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/verify/control/json?request_id=B41F2D19-913C-4BB3-B825-624E375D2C31&cmd=cancel&api_key={1}&api_secret={2}&", ApiUrl, ApiKey, ApiSecret))),
                Times.Once);
            Assert.AreEqual("0", results.status);
            Assert.AreEqual("cancel", results.command);
        }
    }
}