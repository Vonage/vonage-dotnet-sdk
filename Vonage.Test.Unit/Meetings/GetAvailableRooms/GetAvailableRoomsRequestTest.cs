using FluentAssertions;
using Vonage.Meetings.GetAvailableRooms;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetAvailableRooms
{
    public class GetAvailableRoomsRequestTest
    {
        [Theory]
        [InlineData("start", "end")]
        [InlineData("start", null)]
        [InlineData(null, "end")]
        [InlineData(null, null)]
        public void Build_ShouldInitializeValues_GivenValuesAreProvided(string startId, string endId)
        {
            var request = GetAvailableRoomsRequest.Build(startId, endId);
            request.StartId.Should().Be(startId);
            request.EndId.Should().Be(endId);
        }

        [Fact]
        public void Build_ShouldReturnDefaultValues_GivenNoValuesAreProvided()
        {
            var request = GetAvailableRoomsRequest.Build();
            request.StartId.Should().BeNull();
            request.EndId.Should().BeNull();
        }
    }
}