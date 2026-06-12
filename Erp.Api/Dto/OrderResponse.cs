namespace Erp.Api.Dto
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public string ProductCode { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? ReleasedAt { get; set; }
    }
}
