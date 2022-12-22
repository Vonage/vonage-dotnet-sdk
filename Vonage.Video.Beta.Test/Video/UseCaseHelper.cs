using System;
using AutoFixture;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video;
using WireMock.Server;

namespace Vonage.Video.Beta.Test.Video
{
    /// <summary>
    ///     Helper for use cases.
    /// </summary>
    public class UseCaseHelper : IDisposable
    {
        public JsonSerializer Serializer { get; }

        public WireMockServer Server { get; }

        public string Token { get; }

        public Fixture Fixture { get; }

        /// <summary>
        ///     Creates the helper and initialize dependencies.
        /// </summary>
        public UseCaseHelper()
        {
            this.Server = WireMockServer.Start();
            this.Serializer = new JsonSerializer();
            this.Fixture = new Fixture();
            this.Token = this.Fixture.Create<string>();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Server.Stop();
            this.Server.Reset();
            this.Server.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Retrieves the path from a request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <typeparam name="T">The type of the request.</typeparam>
        /// <returns>The path.</returns>
        public static string GetPathFromRequest<T>(Result<T> request) where T : IVideoRequest =>
            request.Match(value => value.GetEndpointPath(), failure => string.Empty);
    }
}