namespace Mes.Api.Dto
{
    public class JobResponse
    {
        public int Id { get; set; }

        public string JobNumber { get; set; } = string.Empty;

        public string ProductCode { get; set; } = string.Empty;

        public int PlannedQuantity { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
