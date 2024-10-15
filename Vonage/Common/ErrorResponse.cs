#region
using System.Net;
#endregion

namespace Vonage.Common;

internal record ErrorResponse(HttpStatusCode Code, string Message);