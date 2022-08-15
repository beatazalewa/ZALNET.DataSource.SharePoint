using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
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
	   // Tutaj używamy klasy ClientSecretCredential z pakietu Azure.Identity:

            ClientSecretCredential clientSecretCredential = new(_azureAppSettings.TenantId,
                                                                _azureAppSettings.ClientId,
                                                                _azureAppSettings.ClientSecret);

	   // Tutaj przekazujemy instancję ClientSecretCredential z pakietu Azure.Identity:
            return new GraphServiceClient(clientSecretCredential);
        }
    }
}
