﻿#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
#endregion

namespace Vonage.Video.Archives.StopArchive;

/// <summary>
///     Represents a request to stop an archive.
/// </summary>
public readonly struct StopArchiveRequest : IVonageRequest, IHasApplicationId, IHasArchiveId
{
    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static ArchiveRequestBuilder<StopArchiveRequest>.IBuilderForApplicationId Build() =>
        ArchiveRequestBuilder<StopArchiveRequest>.Build(tuple => new StopArchiveRequest
        {
            ApplicationId = tuple.Item1,
            ArchiveId = tuple.Item2,
        });

    /// <inheritdoc />
    public Guid ApplicationId { get; private init; }

    /// <inheritdoc />
    public Guid ArchiveId { get; private init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/stop")
            .Build();
}