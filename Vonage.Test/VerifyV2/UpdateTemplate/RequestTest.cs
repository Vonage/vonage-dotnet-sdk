﻿#region
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.UpdateTemplate;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.UpdateTemplate;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        UpdateTemplateRequest.Build()
            .WithId(RequestBuilderTest.ValidTemplateId)
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb");
}