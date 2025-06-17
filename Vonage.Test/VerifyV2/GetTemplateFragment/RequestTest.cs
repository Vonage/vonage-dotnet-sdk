#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.GetTemplateFragment;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplateFragment;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void Parse_ShouldReturnFailure_GivenTemplateFragmentIsEmpty() =>
        GetTemplateFragmentRequest.Parse(Guid.NewGuid(), Guid.Empty)
            .Should()
            .BeParsingFailure("TemplateFragmentId cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenTemplateIdIsEmpty() =>
        GetTemplateFragmentRequest.Parse(Guid.Empty, Guid.NewGuid())
            .Should()
            .BeParsingFailure("TemplateId cannot be empty.");

    [Fact]
    public void Parse_ShouldSetTemplateFragmentId() =>
        GetTemplateFragmentRequest.Parse(Guid.NewGuid(), new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
            .Map(request => request.TemplateFragmentId)
            .Should()
            .BeSuccess(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"));

    [Fact]
    public void Parse_ShouldSetTemplateId() =>
        GetTemplateFragmentRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"), Guid.NewGuid())
            .Map(request => request.TemplateId)
            .Should()
            .BeSuccess(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"));

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        GetTemplateFragmentRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"),
                new Guid("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb"))
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(
                "/v2/verify/templates/f3a065af-ac5a-47a4-8dfe-819561a7a287/template_fragments/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb");
}