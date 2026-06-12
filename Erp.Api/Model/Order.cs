namespace Erp.Api.Model
{
    public class Order
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public string ProductCode { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string Status { get; set; } = "Draft";

        public DateTime CreatedAt { get; set; }

        public DateTime? ReleasedAt { get; set; }
    }
}
