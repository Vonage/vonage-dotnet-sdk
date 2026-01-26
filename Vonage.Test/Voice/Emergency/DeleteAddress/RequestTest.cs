#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.DeleteAddress;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.DeleteAddress;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        DeleteAddressRequest.Build()
            .WithId(new Guid(Constants.ValidAddressId))
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v1/addresses/{Constants.ValidAddressId}");
}