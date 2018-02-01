using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateCatalog.Models
{
    public class CosmosSettings
    {       
        [Required]         
        [Url]
        public string EndpointUrl { get; set; }
        
        [Required] 
        public string AuthorizationKey { get; set; }

        [Required] 
        public string DatabaseId { get; set; }
        
        [Required] 
        public string ContainerId { get; set; }

        public bool IsPopulated
        {
            get
            {
                return !String.IsNullOrEmpty(EndpointUrl) && !String.IsNullOrEmpty(AuthorizationKey) && !String.IsNullOrEmpty(DatabaseId) && !String.IsNullOrEmpty(ContainerId);
            }
        }
    }
}