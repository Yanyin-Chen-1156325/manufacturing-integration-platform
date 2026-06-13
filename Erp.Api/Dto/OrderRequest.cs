using System.ComponentModel.DataAnnotations;

namespace Erp.Api.Dto
{
    public class OrderRequest
    {
        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; } = string.Empty;
        
        [Range(1, 100000)]
        public int Quantity { get; set; }
    }
}
