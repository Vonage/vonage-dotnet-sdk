﻿#region
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.CreateTemplate;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.CreateTemplate;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CreateTemplateRequest.Build()
            .WithName("MyBrand")
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v2/verify/templates");
}