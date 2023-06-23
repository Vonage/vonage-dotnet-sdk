using AutoFixture;
using FluentAssertions;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetAvailableRooms;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetAvailableRooms
{
    public class RequestTest
    {
        private readonly Fixture fixture;

        public RequestTest()
        {
            this.fixture = new Fixture();
        }

        [Fact]
        public void Build_ShouldInitializeValues_GivenEndIdIsNull() =>
            GetAvailableRoomsRequest.Build(this.fixture.Create<string>(), null).EndId.Should().BeNone();

        [Fact]
        public void Build_ShouldInitializeValues_GivenEndIdIsSome() =>
            GetAvailableRoomsRequest.Build(this.fixture.Create<string>(), "Hello").EndId.Should().BeSome("Hello");

        [Fact]
        public void Build_ShouldInitializeValues_GivenStartIdIsNull() =>
            GetAvailableRoomsRequest.Build(null, this.fixture.Create<string>()).StartId.Should().BeNone();

        [Fact]
        public void Build_ShouldInitializeValues_GivenStartIdIsSome() =>
            GetAvailableRoomsRequest.Build("Hello", this.fixture.Create<string>()).StartId.Should().BeSome("Hello");

        [Fact]
        public void Build_ShouldReturnDefaultValues_GivenNoValuesAreProvided()
        {
            var request = GetAvailableRoomsRequest.Build();
            request.StartId.Should().BeNone();
            request.EndId.Should().BeNone();
        }

        [Theory]
        [InlineData(null, null, "/meetings/rooms")]
        [InlineData("", "", "/meetings/rooms")]
        [InlineData(" ", " ", "/meetings/rooms")]
        [InlineData("StartId", null, "/meetings/rooms?start_id=StartId")]
        [InlineData(null, "EndId", "/meetings/rooms?end_id=EndId")]
        [InlineData("Start Id", "End Id", "/meetings/rooms?start_id=Start%20Id&end_id=End%20Id")]
        public void GetEndpointPath_ShouldReturnApiEndpoint(string startId, string endId, string expectedEndpoint) =>
            GetAvailableRoomsRequest.Build(startId, endId).GetEndpointPath().Should().Be(expectedEndpoint);
    }
}