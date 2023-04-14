using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Applications;

public interface IApplicationClient
{
    /// <summary>
    /// Application Name
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<Application> CreateApplicaitonAsync(CreateApplicationRequest request, Credentials creds = null);

    /// <summary>
    /// List applications
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<ApplicationPage> ListApplicationsAsync(ListApplicationsRequest request, Credentials creds = null);

    /// <summary>
    /// Retrieves information about an application
    /// </summary>
    /// <param name="id">Id of the application to be retrieved</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<Application> GetApplicationAsync(string id, Credentials creds = null);

    /// <summary>
    /// Updates an Application
    /// </summary>
    /// <param name="id">Id of the application to be updated</param>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<Application> UpdateApplicationAsync(string id, CreateApplicationRequest request, Credentials creds = null);

    /// <summary>
    /// Deletes an application: Cannot be undone
    /// </summary>
    /// <param name="id">Id of the application to be deleted</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task <bool> DeleteApplicationAsync(string id, Credentials creds = null);

    /// <summary>
    /// Application Name
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Application CreateApplicaiton(CreateApplicationRequest request, Credentials creds = null);

    /// <summary>
    /// List applications
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    ApplicationPage ListApplications(ListApplicationsRequest request, Credentials creds = null);

    /// <summary>
    /// Retrieves information about an application
    /// </summary>
    /// <param name="id">Id of the application to be retrieved</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Application GetApplication(string id, Credentials creds = null);

    /// <summary>
    /// Updates an Application
    /// </summary>
    /// <param name="id">Id of the application to be updated</param>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Application UpdateApplication(string id, CreateApplicationRequest request, Credentials creds = null);

    /// <summary>
    /// Deletes an application: Cannot be undone
    /// </summary>
    /// <param name="id">Id of the application to be deleted</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    bool DeleteApplication(string id, Credentials creds = null);
}