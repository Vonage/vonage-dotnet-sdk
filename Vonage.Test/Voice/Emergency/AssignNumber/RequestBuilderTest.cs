#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.AssignNumber;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.AssignNumber;

public class RequestBuilderTest
{
    private const string ValidNumber = "+33601020304";
    private const string ValidContactName = "John Smith";
    private const string ValidAddressId = "e5fcbdc8-6367-45bd-8d92-7b3ef266e754";
    private const string InvalidNumber = "aa601020304";

    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberIsInvalid() =>
        AssignNumberRequest.Build()
            .WithNumber(InvalidNumber)
            .WithAddressId(new Guid(ValidAddressId))
            .WithContactName(ValidContactName)
            .Create()
            .Should()
            .BeParsingFailure("Number can only contain digits.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenAddressIdIsEmpty() =>
        AssignNumberRequest.Build()
            .WithNumber(ValidNumber)
            .WithAddressId(Guid.Empty)
            .WithContactName(ValidContactName)
            .Create()
            .Should()
            .BeParsingFailure("AddressId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenContactNameIsEmpty() =>
        AssignNumberRequest.Build()
            .WithNumber(ValidNumber)
            .WithAddressId(new Guid(ValidAddressId))
            .WithContactName(string.Empty)
            .Create()
            .Should()
            .BeParsingFailure("ContactName cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetAddressId() =>
        AssignNumberRequest.Build()
            .WithNumber(ValidNumber)
            .WithAddressId(new Guid(ValidAddressId))
            .WithContactName(ValidContactName)
            .Create()
            .Map(request => request.AddressId)
            .Should()
            .BeSuccess(new Guid(ValidAddressId));

    [Fact]
    public void Build_ShouldSetNumber() =>
        AssignNumberRequest.Build()
            .WithNumber(ValidNumber)
            .WithAddressId(new Guid(ValidAddressId))
            .WithContactName(ValidContactName)
            .Create()
            .Map(request => request.Number)
            .Should()
            .BeSuccess(ValidNumber);

    [Fact]
    public void Build_ShouldSetContactName() =>
        AssignNumberRequest.Build()
            .WithNumber(ValidNumber)
            .WithAddressId(new Guid(ValidAddressId))
            .WithContactName(ValidContactName)
            .Create()
            .Map(request => request.ContactName)
            .Should()
            .BeSuccess(ValidContactName);
}