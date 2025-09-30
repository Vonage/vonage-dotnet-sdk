#region
using System;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.VerifyCode;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.VerifyCode;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldSerialize() =>
        VerifyCodeRequest.Build()
            .WithRequestId(Guid.NewGuid())
            .WithCode("123456789")
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}