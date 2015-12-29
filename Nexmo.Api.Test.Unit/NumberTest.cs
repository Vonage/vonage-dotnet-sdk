using System;
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using Nexmo.Api.Request;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class NumberTest : MockedWebTest
    {
        [Test]
        public void should_search_numbers()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"count\":177,\"numbers\":[{\"country\":\"US\",\"msisdn\":\"15102694548\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568490\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568491\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568492\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"},{\"country\":\"US\",\"msisdn\":\"17088568973\",\"type\":\"mobile-lvn\",\"features\":[\"SMS\",\"VOICE\"],\"cost\":\"0.67\"}]}")));
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var results = Number.Search(new Number.SearchRequest
            {
                country = "US"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/number/search/?country=US&api_key={1}&api_secret={2}&", RestUrl, ApiKey, ApiSecret))),
                Times.Once);
            Assert.AreEqual(177, results.count);
            Assert.AreEqual(5, results.numbers.Count());
        }

        [Test]
        public void should_buy_number()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"error-code\":\"200\",\"error-code-label\":\"success\"}")));
            var postDataStream = new MemoryStream();
            _request.Setup(e => e.GetRequestStream()).Returns(postDataStream);
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var result = Number.Buy("US", "17775551212");

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/number/buy", RestUrl))),
                Times.Once);
            postDataStream.Position = 0;
            var sr = new StreamReader(postDataStream);
            var postData = sr.ReadToEnd();
            Assert.AreEqual(string.Format("country=US&msisdn=17775551212&api_key={0}&api_secret={1}&", ApiKey, ApiSecret), postData);

            Assert.AreEqual("200", result.ErrorCode);
        }

        [Test]
        public void should_update_number()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"error-code\":\"200\",\"error-code-label\":\"success\"}")));
            var postDataStream = new MemoryStream();
            _request.Setup(e => e.GetRequestStream()).Returns(postDataStream);
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var result = Number.Update(new Number.NumberUpdateCommand
            {
                country = "US",
                msisdn = "17775551212",
                moHttpUrl = "https://test.test.com/mo",
                moSmppSysType = "inbound"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/number/update", RestUrl))),
                Times.Once);
            postDataStream.Position = 0;
            var sr = new StreamReader(postDataStream);
            var postData = sr.ReadToEnd();
            Assert.AreEqual(string.Format("country=US&msisdn=17775551212&moHttpUrl=https%3a%2f%2ftest.test.com%2fmo&moSmppSysType=inbound&api_key={0}&api_secret={1}&", ApiKey, ApiSecret), postData);

            Assert.AreEqual("200", result.ErrorCode);
        }

        [Test]
        public void should_cancel_number()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"error-code\":\"200\",\"error-code-label\":\"success\"}")));
            var postDataStream = new MemoryStream();
            _request.Setup(e => e.GetRequestStream()).Returns(postDataStream);
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var result = Number.Cancel("US", "17775551212");

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/number/cancel", RestUrl))),
                Times.Once);
            postDataStream.Position = 0;
            var sr = new StreamReader(postDataStream);
            var postData = sr.ReadToEnd();
            Assert.AreEqual(string.Format("country=US&msisdn=17775551212&api_key={0}&api_secret={1}&", ApiKey, ApiSecret), postData);

            Assert.AreEqual("200", result.ErrorCode);
        }
    }
}