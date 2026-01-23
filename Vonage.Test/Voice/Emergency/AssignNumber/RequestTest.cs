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
    private const string ValidNumber = "+33601020304";
    private const string ValidContactName = "John Smith";
    private const string ValidGuid = "e5fcbdc8-6367-45bd-8d92-7b3ef266e754";

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        AssignNumberRequest.Build().WithNumber(ValidNumber)
            .WithAddressId(new Guid(ValidGuid))
            .WithContactName(ValidContactName)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/emergency/numbers/33601020304");
}