using System.ComponentModel.DataAnnotations;

namespace ZALNET.DataSource.SharePoint
{
    internal class MsGraphConfiguration 
    { 
        [Required] 
        public string TenantId { get; set; } 
        [Required]
        public string CredentialsClientId { get; set; }
        [Required]
        public string CredentialsClientSecret { get; set; }
        [Required] 
        public string TenantName { get; set; } 
    }
}
