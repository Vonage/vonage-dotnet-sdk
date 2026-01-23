#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.AssignNumber;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.AssignNumber;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        AssignNumberRequest.Build().WithNumber(Constants.ValidNumber)
            .WithAddressId(new Guid(Constants.ValidAddressId))
            .WithContactName(Constants.ValidContactName)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/emergency/numbers/33601020304");
}