#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.GetTemplate;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplate;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetTemplateRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v2/verify/templates/f3a065af-ac5a-47a4-8dfe-819561a7a287");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenRequestIsEmpty() =>
        GetTemplateRequest.Parse(Guid.Empty)
            .Should()
            .BeParsingFailure("TemplateId cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnSuccess() =>
        GetTemplateRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
            .Map(request => request.TemplateId)
            .Should()
            .BeSuccess(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"));
}