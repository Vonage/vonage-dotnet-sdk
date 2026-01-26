#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.GetAddress;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.GetAddress;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetAddressRequest.Build()
            .WithId(new Guid(Constants.ValidAddressId))
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v1/addresses/{Constants.ValidAddressId}");
}