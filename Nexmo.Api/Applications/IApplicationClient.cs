using Nexmo.Api.Request;

namespace Nexmo.Api.Applications
{
    public interface IApplicationClient
    {
        Application CreateApplicaiton(CreateApplicationRequest request, Credentials creds = null);
        ApplicationPage ListApplications(ListApplicationsRequest request, Credentials creds = null);
        Application GetApplication(string id, Credentials creds = null);
        Application UpdateApplication(string id, CreateApplicationRequest request, Credentials creds = null);
        bool DeleteApplication(string id, Credentials creds = null);
    }
}