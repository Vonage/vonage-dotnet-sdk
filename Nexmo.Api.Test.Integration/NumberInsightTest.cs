using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nexmo.Api.Test.Integration
{
    [TestClass]
    public class NumberInsightTest
    {
        [TestMethod]
        public void should_send_basic_ni_request()
        {
            var result = NumberInsight.RequestBasic(new NumberInsight.NumberInsightRequest
            {
                Number = Configuration.Instance.Settings["test_number"]
            });
            Assert.AreEqual("0", result.Status);
            Assert.AreEqual(Configuration.Instance.Settings["test_number"], result.InternationalFormatNumber);
            Assert.AreEqual("(555) 555-1212", result.NationalFormatNumber);
            
        }

        [TestMethod]
        public void should_send_standard_ni_request()
        {
            var result = NumberInsight.RequestStandard(new NumberInsight.NumberInsightRequest
            {
                Number = Configuration.Instance.Settings["test_number"]
            });
            Assert.AreEqual("0", result.Status);
            Assert.AreEqual(Configuration.Instance.Settings["test_number"], result.InternationalFormatNumber);
            Assert.AreEqual("(555) 555-1212", result.NationalFormatNumber);
            Assert.AreEqual("Verizon Wireless", result.CurrentCarrier.Name);
        }
    }
}