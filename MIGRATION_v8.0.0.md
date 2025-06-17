# Breaking changes in version 8.0.0

This document covers all breaking changes introduced with v8.0.0.

If you migrate from v7.X.X to v8.X.X (or above), you will have to handle the following points in your codebase.

#### Removing Obsolete APIs and members

When upgrading to a newer release, you may have noticed a warning indicating you're using an obsolete method.
Indeed, we flagged these changes with an `Obsolete` tag:

* Meetings API has been sunset.
* Proactive Connect API has been sunset.
* `fraud_score` in NumberInsights V2 has been sunset; SimSwap is the only insight.

#### Remove explicit builder implementation (where possible)

Rely on source generators as much as possible.
This will, in some cases, change the method name of some builder methods to strictly follow the `.With{PropertyName}`
convention.

* Video.CreateArchiveRequest

#### Remove GetEndpointPath() on Vonage Requests

This method has been transformed into an implementation details, and won't be publicly available anymore.

```csharp
public interface IVonageRequest
{
    string GetEndpointPath();
}
```

If you need to retrieve the URL of a request, you can use `HttpRequestMessage BuildRequestMessage()` and retrieve the
relative URI directly on `HttpRequestMessage`.