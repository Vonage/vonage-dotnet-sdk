using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nexmo.Api.Test.Integration
{
    [TestClass]
	public class ThrottleTest
    {
        [TestMethod]
        public void should_rate_limit()
        {
            Configuration.Instance.Settings["appSettings:Nexmo.Api.RequestsPerSecond"] = "1";
            var watch = new Stopwatch();
            watch.Start();
            for (var i = 1; i < 7; i++)
            {
                Account.GetBalance();
            }
            watch.Stop();
            Assert.IsTrue(watch.Elapsed.Seconds >= 5);
        }
    }
}