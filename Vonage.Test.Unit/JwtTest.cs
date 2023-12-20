using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Request;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Test.Unit.Common.TestHelpers;
using Xunit;

namespace Vonage.Test.Unit
{
    public class JwtTest
    {
        private const string ApplicationId = "ffffffff-ffff-ffff-ffff-ffffffffffff";
        private readonly string privateKey = TokenHelper.GetKey();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GenerateToken_ShouldReturnAuthenticationFailure_GivenPrivateKeyIsNullOrWhitespace(string key) =>
            new Jwt().GenerateToken(ApplicationId, key).Should()
                .BeFailure(new AuthenticationFailure("AppId or Private Key Path missing."));

        [Fact]
        public void GenerateToken_ShouldReturnSuccess_GivenCredentialsAreProvided() =>
            new Jwt().GenerateToken(Credentials.FromAppIdAndPrivateKey(ApplicationId, this.privateKey)).Should()
                .BeSuccess(success => success.Should().NotBeEmpty());

        [Fact]
        public void GenerateToken_ShouldReturnSuccess_GivenIdAndKeyAreProvided() =>
            new Jwt().GenerateToken(ApplicationId, this.privateKey).Should()
                .BeSuccess(success => success.Should().NotBeEmpty());

        [Fact]
        public void TestJwt()
        {
            var token = Jwt.CreateToken(ApplicationId, this.privateKey);
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void VerifySignature_ShouldReturnFalse_GivenPrivateKeyIsEmpty(string emptyKey) => Jwt
            .VerifySignature(Jwt.CreateToken(ApplicationId, this.privateKey), emptyKey).Should().BeFalse();

        [Fact]
        public void VerifySignature_ShouldReturnFalse_GivenTokenGeneratedFromDifferentKey()
        {
            const string secondKey =
                "-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCeYVYiVAxn4Iza\nGNWhhCKxp1g0k3WiGmSyWVlYPgo+hPmsN3+ya3VBUMOlbvhLH/SgAZZR1Dzga8Rl\nNZEHlFnEUkj/Is8ffAze1WCHxqgOF5B0/WrxFK1ElVbuTi/eCsmNht/+cTvAAHQm\nwKwZLPMgrGdnfhvy3/Wa5JpyIq93HjgeDtKLsQuH2H6/LFmWhRXzwrNfItXccTmA\nX9Q87w9+pHQAkFuSMAO3YSY3G20ZTE63Kyr+tz9hYtSeInz+joKFO9tBoE2lDerX\nbKnFb2n4+zPuObCVo7veyxMFu+0BM5P8jmmERJEzxa6QPmQnTL+x/H249UMBXTm+\nJZSXrPUXAgMBAAECggEAOdOvR9RpPSo7M0x6/8CHjFbd+UBX5Gp/lrDR0srAIcia\ngns3CDV89+6dqJMGXCOcRSmfMVxtJ8GhURTIUbykM+6ZUXJrroK0Dk5ZMunhJVgM\nOfLnL4PNqatfNJ5ektNceOykbzn3x2hDAH07uOt1D9py3NIqMTj9a2MJUx//8d0G\ndgwfdvytX7drq8Kbm5vrEb59+gNZV2Zl/6RMwfqX7BWphZAoUjpGRUV8skTz32Ns\nFc8ASQFx9lfHEPXLEtV9Eb8x23wAxB1Z360z1Epze33Ncu+GIAWgGjmEHAK0AGM6\n+unK4/Rwyp8f/fr5jaIFs9pZ/6IYCuAa1OYBavXNeQKBgQDesHoX4N1KsHsWrY8T\nQizCzooWFArI5ehK06OnNLgWTMVwY0AxXEbmA44m1QWyjCdz1/PbyEHud264Yob1\nh1I6sQzerHDB8H7bw3uNmZ2AeTyjHxVpm/MAT757t3Lkreed0dY+R2Mu3/h4ls5D\ncJBsFBDbAtdAcuLyMOl/zbPJiQKBgQC2Ej8kBnxBu6nHfLWHnBFX6kr9/fWXmkzb\nExhyUBbQjfPb7QVMOPQMyZ1H01fnK9qIUc1FFMV9w7FpRT46EjgdUi7JoQktlsiV\nxw5T46UGAYEBxGJ2PFQxveb0ke3GGxWIwFoDXfeqoJ41//8eZtqsv2wfDMu5w5bH\nN6Mt/V9BnwKBgBWJN6WLh5srihvdWRLhuXZ1hwEvmwNmyJpD8XXAMXVmo0mFL7YW\nWHzyJxM2UsC7sS0Q23KL4WRwhHKshKTGG3u1l6sJPjZjTcFvHEnLa2H7V5Pm86ks\n/ckv7RoF2cSn1Nh8bJ3FRaBI5Ly0yOnYvv7UyfSER9Dhy3TkqhBEEnkRAoGAO/xX\nAS+B0MZbcXYM+NjaffSbMINzXrdfiu4Hp+HrIlsidNrqW+qHvY7PWtKuq2MvZKpO\nzuvBBWZsFRrTHZ7TDhX9hECiHXsIKxCiD9F4lTn5nvNs3TeTJbBDX2CUziguOBfn\nkCRtbLHubiwhiYqpkTCgFrrIhDOEga46/PP1ZlECgYA1FPc2DsNV2pc/lnWM3hEK\n69isIZ2YNpAGWIaoNAiMeOnR3lGgUEQR9hoAci8iRtYgNsq6zNldYH1h0HK0i6qy\n4UHLHdZKhTY1LGE7NBukVZJXgP71NyoR2JlWh2/MCwukeGaK7rhqc9eeqm7rSr1l\nWhYY2KCcZ7BPijAGrG6IQQ==\n-----END PRIVATE KEY-----";
            Jwt.VerifySignature(Jwt.CreateToken(ApplicationId, this.privateKey), secondKey).Should().BeFalse();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void VerifySignature_ShouldReturnFalse_GivenTokenIsEmpty(string emptyToken) =>
            Jwt.VerifySignature(emptyToken, this.privateKey).Should().BeFalse();

        [Fact]
        public void VerifySignature_ShouldReturnTrue_GivenTokenIsValid() => Jwt
            .VerifySignature(Jwt.CreateToken(ApplicationId, this.privateKey), this.privateKey).Should().BeTrue();
    }
}