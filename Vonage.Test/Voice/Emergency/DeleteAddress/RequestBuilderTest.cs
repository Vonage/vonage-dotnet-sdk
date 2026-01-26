#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.DeleteAddress;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.DeleteAddress;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldReturnFailure_GivenIdIsEmpty() =>
        DeleteAddressRequest.Build()
            .WithId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("Id cannot be empty.");

    [Fact]
    public void Build_ShouldSetId() =>
        DeleteAddressRequest.Build()
            .WithId(new Guid(Constants.ValidAddressId))
            .Create()
            .Map(request => request.Id)
            .Should()
            .BeSuccess(new Guid(Constants.ValidAddressId));
}