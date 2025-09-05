#region
using FluentAssertions;
using Vonage.Cryptography;
using Xunit;
#endregion

namespace Vonage.Test.Messaging;

public class InboundSmsTest
{
    private const string SigningSecret = "Y6dI3wtDP8myVH5tnDoIaTxEvAJhgDVCczBa1mHniEqsdlnnebg";

    [Theory]
    [InlineData(SmsSignatureGenerator.Method.md5)]
    [InlineData(SmsSignatureGenerator.Method.md5hash)]
    [InlineData(SmsSignatureGenerator.Method.sha1)]
    [InlineData(SmsSignatureGenerator.Method.sha256)]
    [InlineData(SmsSignatureGenerator.Method.sha512)]
    public void TestValidateSignatureMd5(SmsSignatureGenerator.Method encryptionMethod) =>
        InboundSmsTestData.CreateWithValidSignature(encryptionMethod).ValidateSignature(SigningSecret, encryptionMethod)
            .Should().BeTrue();
}