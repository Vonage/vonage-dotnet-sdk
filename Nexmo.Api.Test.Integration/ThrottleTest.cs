using System.Diagnostics;
using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class ThrottleTest
    {
        [Test]
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
            Assert.GreaterOrEqual(watch.Elapsed.Seconds, 5);
        }
    }
}