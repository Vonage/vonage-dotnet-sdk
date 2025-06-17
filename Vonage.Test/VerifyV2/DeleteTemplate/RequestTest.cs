#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.DeleteTemplate;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.DeleteTemplate;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void Parse_ShouldReturnFailure_GivenRequestIsEmpty() =>
        DeleteTemplateRequest.Parse(Guid.Empty)
            .Should()
            .BeParsingFailure("TemplateId cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnSuccess() =>
        DeleteTemplateRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
            .Map(request => request.TemplateId)
            .Should()
            .BeSuccess(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"));

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        DeleteTemplateRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/verify/templates/f3a065af-ac5a-47a4-8dfe-819561a7a287");
}