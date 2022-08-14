using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ZALNET.DataSource.SharePoint.Settings;

namespace ZALNET.DataSource.SharePoint
{
    public class GraphServiceClientProvider
    {
        private readonly AzureApp _azureAppSettings;

        public GraphServiceClientProvider(IOptions<AppSettings> settings)
        {
            _azureAppSettings = settings.Value.AzureApp;
        }

        public GraphServiceClient Create()
        {
            var confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(_azureAppSettings.ClientId)
                .WithClientSecret(_azureAppSettings.ClientSecret)
                .WithTenantId(_azureAppSettings.TenantId)
                .Build();

            var authProvider = new ClientCredentialProvider(confidentialClientApplication);
            return new GraphServiceClient(authProvider);
        }
        //            // The Azure AD tenant ID  (e.g. tenantId.onmicrosoft.com)
        //            var tenantId = _azureAppSettings.TenantId;
        //            // The client ID of the app registered in Azure AD
        //            var clientId = _azureAppSettings.ClientId;
        //            // Application Client Secret (Recommended this is stored safely and not hardcoded)
        //            var clientSecret = _azureAppSettings.ClientSecret;
        //            var scopes = new string[] { "https://graph.microsoft.com/.default" };
        //            var confidentialClient = ConfidentialClientApplicationBuilder
        //                .Create(clientId)
        //                .WithAuthority($"https://login.microsoftonline.com/$tenantId/v2.0")
        //                .WithClientSecret(clientSecret)
        //                .Build();
        //            GraphServiceClient graphServiceClient =
        //new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) => {

        //    // Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
        //    var authResult = await confidentialClient.AcquireTokenForClient(scopes).ExecuteAsync();

        //    // Add the access token in the Authorization header of the API
        //    requestMessage.Headers.Authorization =
        //    new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
        //}));
        //            // Make a Microsoft Graph API query
        //            var users = await graphServiceClient.Users.Request().GetAsync();
        //            return (GraphServiceClient)users;
    }
}
