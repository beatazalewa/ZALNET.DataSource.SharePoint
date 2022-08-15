using System.ComponentModel.DataAnnotations;

namespace ZALNET.DataSource.SharePoint.Settings
{
    public class ListDataSettings
    {
        [Required]
        public string SiteUrl { get; set; }
        [Required]
        public string ListName { get; set; }
    }
}