using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZALNET.DataSource.SharePoint.Settings;

namespace ZALNET.DataSource.SharePoint
{
    public class ListData
    {
        private readonly ListDataSettings _settings;
        private readonly GraphServiceClient _graphServiceClient;

        public ListData(IOptions<ListDataSettings> settings, GraphServiceClient graphServiceClient)
        {
            _settings = settings.Value;
            _graphServiceClient = graphServiceClient;
        }

        [FunctionName("SharePoint-ListData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ListData is requested.");

            var list = await _graphServiceClient.GetListAsync(_settings.SiteUrl, _settings.ListName);

            var queryOptions = new List<QueryOption>()
            {
                //new QueryOption("filter", "fields/Title eq 'item 1'"),
                new QueryOption("select", "ID"),
                new QueryOption("expand", "fields(select=Question,Answer)")
            };
            var itemsPage = await list.Items
                .Request(queryOptions)
                .GetAsync();
            var items = new List<ListItem>(itemsPage);

            while (itemsPage.NextPageRequest != null)
            {
                itemsPage = await itemsPage.NextPageRequest.GetAsync();
                items.AddRange(itemsPage);
            }

            return new OkObjectResult(items);
        }
    }
}
