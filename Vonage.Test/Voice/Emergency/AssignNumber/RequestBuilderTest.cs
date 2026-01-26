#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.AssignNumber;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.AssignNumber;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberIsInvalid() =>
        AssignNumberRequest.Build()
            .WithNumber(Constants.InvalidNumber)
            .WithAddressId(new Guid(Constants.ValidAddressId))
            .WithContactName(Constants.ValidContactName)
            .Create()
            .Should()
            .BeParsingFailure("Number can only contain digits.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenAddressIdIsEmpty() =>
        AssignNumberRequest.Build()
            .WithNumber(Constants.ValidNumber)
            .WithAddressId(Guid.Empty)
            .WithContactName(Constants.ValidContactName)
            .Create()
            .Should()
            .BeParsingFailure("AddressId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenContactNameIsEmpty() =>
        AssignNumberRequest.Build()
            .WithNumber(Constants.ValidNumber)
            .WithAddressId(new Guid(Constants.ValidAddressId))
            .WithContactName(string.Empty)
            .Create()
            .Should()
            .BeParsingFailure("ContactName cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetAddressId() =>
        AssignNumberRequest.Build()
            .WithNumber(Constants.ValidNumber)
            .WithAddressId(new Guid(Constants.ValidAddressId))
            .WithContactName(Constants.ValidContactName)
            .Create()
            .Map(request => request.AddressId)
            .Should()
            .BeSuccess(new Guid(Constants.ValidAddressId));

    [Fact]
    public void Build_ShouldSetNumber() =>
        AssignNumberRequest.Build()
            .WithNumber(Constants.ValidNumber)
            .WithAddressId(new Guid(Constants.ValidAddressId))
            .WithContactName(Constants.ValidContactName)
            .Create()
            .Map(request => request.Number)
            .Should()
            .BeSuccess(Constants.ValidNumber);

    [Fact]
    public void Build_ShouldSetContactName() =>
        AssignNumberRequest.Build()
            .WithNumber(Constants.ValidNumber)
            .WithAddressId(new Guid(Constants.ValidAddressId))
            .WithContactName(Constants.ValidContactName)
            .Create()
            .Map(request => request.ContactName)
            .Should()
            .BeSuccess(Constants.ValidContactName);
}