#region
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency;
using Vonage.Voice.Emergency.CreateAddress;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.CreateAddress;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldSerialize_GivenEmpty() =>
        GetEmptyRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    internal static Result<CreateAddressRequest> GetEmptyRequest() => CreateAddressRequest.Build().Create();

    internal static Result<CreateAddressRequest> GetFullRequest() => CreateAddressRequest.Build()
        .WithName("MyAddress")
        .WithFirstAddressLine("1 REGAL CT")
        .WithSecondAddressLine("Merchant's House 205")
        .WithCity("New York")
        .WithRegion("NJ")
        .WithLocation(Address.AddressLocationType.Business)
        .WithPostalCode("10002")
        .WithCountry("US")
        .Create();

    [Fact]
    public void ShouldSerialize_GivenFull() =>
        GetFullRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}