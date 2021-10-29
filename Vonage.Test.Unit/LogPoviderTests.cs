using Microsoft.Extensions.Logging;
using Moq;
using Vonage.Logger;
using Xunit;

namespace Vonage.Test.Unit
{
    public class LogPoviderTests : TestBase
    {
        [Fact]
        public void SetLogFactory()
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>(MockBehavior.Strict);

            Mock<ILoggerFactory> mockLoggerFactory = new Mock<ILoggerFactory>(MockBehavior.Strict);
            mockLoggerFactory
                .Setup(x => x.CreateLogger(It.Is<string>(y => y.Equals("test1"))))
                .Returns(mockLogger.Object);

            LogProvider.SetLogFactory(mockLoggerFactory.Object);
            var logger = LogProvider.GetLogger("test1");

            Assert.Same(mockLogger.Object, logger);
        }
    }
}
