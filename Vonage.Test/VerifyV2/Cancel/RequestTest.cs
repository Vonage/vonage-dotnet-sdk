using System;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.Cancel;
using Xunit;

namespace Vonage.Test.VerifyV2.Cancel;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CancelRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v2/verify/f3a065af-ac5a-47a4-8dfe-819561a7a287");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenRequestIsEmpty() =>
        CancelRequest.Parse(Guid.Empty)
            .Should()
            .BeParsingFailure("RequestId cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnSuccess() =>
        CancelRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
            .Map(request => request.RequestId)
            .Should()
            .BeSuccess(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"));
}