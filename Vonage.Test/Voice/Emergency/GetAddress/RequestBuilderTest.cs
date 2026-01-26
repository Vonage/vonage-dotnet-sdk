#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.GetAddress;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.GetAddress;

public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldReturnFailure_GivenIdIsEmpty() =>
        GetAddressRequest.Build()
            .WithId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("Id cannot be empty.");

    [Fact]
    public void Build_ShouldSetId() =>
        GetAddressRequest.Build()
            .WithId(new Guid(Constants.ValidAddressId))
            .Create()
            .Map(request => request.Id)
            .Should()
            .BeSuccess(new Guid(Constants.ValidAddressId));
}