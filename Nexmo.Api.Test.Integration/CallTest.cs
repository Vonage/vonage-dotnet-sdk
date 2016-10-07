using System.Configuration;
using System.Net;
using Nexmo.Api.Voice;
using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class CallTest
    {
        [Test]
        public void should_call()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            var results = Call.Do(new Call.CallCommand
            {
                to = new[]
                {
                    new Call.Endpoint {
                        type = "phone",
                        number = ConfigurationManager.AppSettings["test_number"]
                    }
                },
                from = new Call.Endpoint
                {
                    type = "phone",
                    number = ConfigurationManager.AppSettings["nexmo_number"]
                },
                answer_url = new[]
                {
                    "https://nexmo-community.github.io/ncco-examples/first_call_talk.json"
                }
            });
            Assert.AreEqual("started", results.status);
        }

        [Test]
        public void should_get_calls()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            var results = Call.List();
            Assert.AreEqual(3, results._embedded.calls.Count);
        }

        [Test]
        public void should_get_specified_call()
        {
            var id = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            var results = Call.Get(id);
            Assert.AreEqual(id, results.uuid);
        }

        [Test]
        public void should_edit_specified_call()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            var id = "ffffffff-ffff-ffff-ffff-ffffffffffff";
            var results = Call.Edit(id, new Call.CallEditCommand
            {
                action = "hangup"
            });
            Assert.AreEqual(id, results.uuid);
        }
    }
}