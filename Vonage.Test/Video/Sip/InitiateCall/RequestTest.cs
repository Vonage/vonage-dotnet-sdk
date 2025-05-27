#region
using System;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sip.InitiateCall;
using Xunit;
#endregion

namespace Vonage.Test.Video.Sip.InitiateCall;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid applicationId;
    private readonly string sessionId;
    private readonly SipElement sip;
    private readonly string token;
    private readonly Uri uri;

    public RequestTest()
    {
        var fixture = new Fixture();
        fixture.Customize(new SupportMutableValueTypesCustomization());
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.token = fixture.Create<string>();
        this.sip = fixture.Create<SipElement>();
        this.uri = fixture.Create<Uri>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        InitiateCallRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithToken(this.token)
            .WithSipUri(this.uri)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.applicationId}/dial");
}