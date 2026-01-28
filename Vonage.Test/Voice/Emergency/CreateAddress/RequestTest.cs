#region
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.CreateAddress;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/addresses");
}