using System.ComponentModel.DataAnnotations;

namespace Mes.Api.Dto
{
    public class JobRequest
    {
        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; } = string.Empty;

        [Range(1, 100000)]
        public int PlannedQuantity { get; set; }
    }
}
