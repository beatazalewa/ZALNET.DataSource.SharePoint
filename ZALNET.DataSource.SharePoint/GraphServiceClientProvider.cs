using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
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
    }
}
