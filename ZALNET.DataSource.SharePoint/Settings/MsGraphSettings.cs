using System.ComponentModel.DataAnnotations;

namespace ZALNET.DataSource.SharePoint.Settings
{
    public class MsGraphSettings
    {
        [Required]
        public string TenantId { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
}
