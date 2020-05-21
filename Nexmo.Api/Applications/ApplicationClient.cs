using Nexmo.Api.Request;

namespace Nexmo.Api.Applications
{
    public class ApplicationClient : IApplicationClient
    {
        public Credentials Credentials { get; set; }
        public ApplicationClient(Credentials creds)
        {
            Credentials = creds;
        }
        public Application CreateApplicaiton(CreateApplicationRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<Application>(
                "POST",
                ApiRequest.GetBaseUriFor(typeof(ApplicationV2), "/v2/applications"),
                request,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials
            );
        }

        public ApplicationPage ListApplications(ListApplicationsRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<ApplicationPage>(
                ApiRequest.GetBaseUriFor(typeof(ApplicationV2), "/v2/applications"),
                ApiRequest.AuthType.Basic,
                request,
                creds ?? Credentials
            );
        }

        public Application GetApplication(string id, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<Application>(
                ApiRequest.GetBaseUriFor(typeof(ApplicationV2), $"/v2/applications/{id}"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials
            );
        }

        public Application UpdateApplication(string id, CreateApplicationRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<Application>(
                "PUT",
                ApiRequest.GetBaseUriFor(typeof(ApplicationV2), $"/v2/applications/{id}"),
                request,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials
            );
        }

        public bool DeleteApplication(string id, Credentials creds = null)
        {
            ApiRequest.DoDeleteRequestWithUrlContent(
                ApiRequest.GetBaseUriFor(typeof(ApplicationV2), $"/v2/applications/{id}"),
                null,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials
            );
            return true;
        }
    }
}