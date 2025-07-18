﻿#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
#endregion

namespace Vonage.Video.Archives.GetArchive;

/// <summary>
///     Represents a request to retrieve an archive.
/// </summary>
public readonly struct GetArchiveRequest : IVonageRequest, IHasApplicationId, IHasArchiveId
{
    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static ArchiveRequestBuilder<GetArchiveRequest>.IBuilderForApplicationId Build() =>
        ArchiveRequestBuilder<GetArchiveRequest>.Build(tuple => new GetArchiveRequest
        {
            ApplicationId = tuple.Item1,
            ArchiveId = tuple.Item2,
        });

    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public Guid ArchiveId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}")
            .Build();
}