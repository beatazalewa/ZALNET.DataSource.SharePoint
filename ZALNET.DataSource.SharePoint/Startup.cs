using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using ZALNET.DataSource.SharePoint.Core.DependencyInjection;

[assembly: FunctionsStartup(typeof(ZALNET.DataSource.SharePoint.Startup))]
namespace ZALNET.DataSource.SharePoint
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            //var configuration = builder.GetContext().Configuration;
            services.AddMsGraphServices();
        }
    }
}
