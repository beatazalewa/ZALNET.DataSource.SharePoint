using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using ZALNET.DataSource.SharePoint.Settings;

namespace ZALNET.DataSource.SharePoint.Core.DependencyInjection
{
    internal static class MsGraphServicesCollectionExtensions
    {
        public static IServiceCollection AddMsGraphServices(this IServiceCollection services)
        {
            IConfiguration configuration = services
                                           .BuildServiceProvider()
                                           .GetRequiredService<IConfiguration>();

            // Config explanation:
            // https://docs.microsoft.com/en-us/dotnet/core/extensions/options#options-validation
            services.AddOptions<MsGraphSettings>()
                    .Bind(configuration.GetSection("MsGraphSettings"))
                    .ValidateDataAnnotations();

            services.AddOptions<ListDataSettings>()
                    .Bind(configuration.GetSection("ListDataSettings"))
                    .ValidateDataAnnotations();

            services.AddSingleton(implementationFactory =>
            {
                var msGraphConfiguration = services.BuildServiceProvider()
                                            .GetRequiredService<IOptions<MsGraphSettings>>()
                                            .Value;

                ClientSecretCredential clientSecretCredential = new(msGraphConfiguration.TenantId,
                                                                    msGraphConfiguration.ClientId,
                                                                    msGraphConfiguration.ClientSecret);

                return new GraphServiceClient(clientSecretCredential);
            });

            return services;
        }
    }
}
