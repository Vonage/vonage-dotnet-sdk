using WireMock.RequestBuilders;

namespace Vonage.Video.Beta.Test.Extensions
{
    public static class WireMockExtensions
    {
        public static IRequestBuilder BuildRequestWithAuthenticationHeader(string token) =>
            WireMock.RequestBuilders.Request
                .Create()
                .WithAuthenticationHeader(token);

        public static IRequestBuilder WithAuthenticationHeader(this IRequestBuilder builder, string token) =>
            builder.WithHeader("Authorization", $"Bearer {token}");
    }
}