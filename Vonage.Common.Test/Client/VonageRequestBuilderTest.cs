using AutoFixture;
using FluentAssertions;
using Vonage.Common.Client;

namespace Vonage.Common.Test.Client;

public class VonageRequestBuilderTest
{
    private readonly HttpMethod method;
    private readonly string token;
    private readonly string stringContent;
    private readonly Uri endpointUri;

    public VonageRequestBuilderTest()
    {
        var fixture = new Fixture();
        this.method = fixture.Create<HttpMethod>();
        this.endpointUri = fixture.Create<Uri>();
        this.token = fixture.Create<string>();
        this.stringContent = fixture.Create<string>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldNotUpdateAuthorizationHeader_GivenTokenIsNullOrWhitespace(string invalidToken) =>
        VonageRequestBuilder
            .Initialize(this.method, this.endpointUri.AbsoluteUri)
            .WithAuthorizationToken(invalidToken)
            .Build()
            .Headers
            .Authorization
            .Should()
            .BeNull();

    [Fact]
    public void Build_ShouldReturnRequestNotUpdateContent_GivenContentIsNull() =>
        VonageRequestBuilder
            .Initialize(this.method, this.endpointUri.AbsoluteUri)
            .WithContent(null)
            .Build()
            .Content
            .Should()
            .BeNull();

    [Fact]
    public void Build_ShouldReturnRequestWithAuthorizationHeader_GivenTokenIsProvided()
    {
        var request = VonageRequestBuilder
            .Initialize(this.method, this.endpointUri.AbsoluteUri)
            .WithAuthorizationToken(this.token)
            .Build();
        request.Headers.Authorization.Scheme.Should().Be("Bearer");
        request.Headers.Authorization.Parameter.Should().Be(this.token);
    }

    [Fact]
    public async Task Build_ShouldReturnRequestWithContent_GivenContentIsProvided()
    {
        var request = VonageRequestBuilder
            .Initialize(this.method, this.endpointUri.AbsoluteUri)
            .WithContent(new StringContent(this.stringContent))
            .Build();
        var result = await request.Content.ReadAsStringAsync();
        result.Should().Be(this.stringContent);
    }

    [Fact]
    public void Build_ShouldReturnRequestWithMethodAndUri_GivenMandatoryFieldsAreProvided()
    {
        var request = VonageRequestBuilder
            .Initialize(this.method, this.endpointUri.AbsoluteUri)
            .Build();
        request.Method.Should().Be(this.method);
        request.RequestUri.Should().Be(this.endpointUri);
        request.Headers.Authorization.Should().BeNull();
        request.Content.Should().BeNull();
    }
}