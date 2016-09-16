using System.Linq;
using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class ApplicationTest
    {
        [Test]
        public void should_create_application()
        {
            var result = Application.Create(new ApplicationRequest
            {
                name = "csharptest",
                type = "voice",
                answer_url = "https://abcdefg.ngrok.io/api/voice",
                event_url = "https://abcdefg.ngrok.io/api/voice",
            });
            Assert.AreEqual("csharptest", result.name);
        }

        [Test]
        public void should_get_list_of_applications()
        {
            var result = Application.List();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("csharptest", result.First().name);
        }

        [Test]
        public void should_get_application()
        {
            var result = Application.List(AppId: "ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("csharptest", result.First().name);
        }

        [Test]
        public void should_update_application()
        {
            var result = Application.Update(new ApplicationRequest
            {
                id = "ffffffff-ffff-ffff-ffff-ffffffffffff",
                name = "woocsharptest",
                type = "voice",
                answer_url = "https://abcdefg.ngrok.io/api/voice",
                event_url = "https://abcdefg.ngrok.io/api/voice",
            });
            Assert.AreEqual("woocsharptest", result.name);
        }

        [Test]
        public void should_delete_application()
        {
            var isDeleted = Application.Delete("ffffffff-ffff-ffff-ffff-ffffffffffff");
            Assert.AreEqual(true, isDeleted);
        }
    }
}