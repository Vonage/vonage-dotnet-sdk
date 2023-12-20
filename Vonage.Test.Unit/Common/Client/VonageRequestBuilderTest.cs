using System.Net.Http;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Client;

namespace Vonage.Common.Test.Client
{
    public class VonageRequestBuilderTest
    {
        private readonly HttpMethod method;
        private readonly string stringContent;
        private readonly Uri endpointUri;

        public VonageRequestBuilderTest()
        {
            var fixture = new Fixture();
            this.method = fixture.Create<HttpMethod>();
            this.endpointUri = fixture.Create<Uri>();
            fixture.Create<string>();
            this.stringContent = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldReturnRequestNotUpdateContent_GivenContentIsNull() =>
            VonageRequestBuilder
                .Initialize(this.method, this.endpointUri)
                .WithContent(null)
                .Build()
                .Content
                .Should()
                .BeNull();

        [Fact]
        public void Build_ShouldReturnRequestWithAbsoluteUri_GivenUriIsProvided() =>
            VonageRequestBuilder
                .Initialize(this.method, this.endpointUri)
                .Build()
                .RequestUri
                .IsAbsoluteUri
                .Should()
                .BeTrue();

        [Fact]
        public async Task Build_ShouldReturnRequestWithContent_GivenContentIsProvided()
        {
            var request = VonageRequestBuilder
                .Initialize(this.method, this.endpointUri)
                .WithContent(new StringContent(this.stringContent))
                .Build();
            var result = await request.Content.ReadAsStringAsync();
            result.Should().Be(this.stringContent);
        }

        [Fact]
        public void Build_ShouldReturnRequestWithMethodAndUri_GivenMandatoryFieldsAreProvided()
        {
            var request = VonageRequestBuilder
                .Initialize(this.method, this.endpointUri)
                .Build();
            request.Method.Should().Be(this.method);
            request.RequestUri.Should().Be(this.endpointUri);
            request.Headers.Authorization.Should().BeNull();
            request.Content.Should().BeNull();
        }

        [Fact]
        public void Build_ShouldReturnRequestWithRelativeUri_GivenStringIsProvided() =>
            VonageRequestBuilder
                .Initialize(this.method, "/api/fakeEndpoint")
                .Build()
                .RequestUri
                .IsAbsoluteUri
                .Should()
                .BeFalse();
    }
}