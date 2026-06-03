namespace ISO_ERP.Models
{
    public class Production
    {
        public int Id { get; set; }

        public string BatchNo { get; set; } = string.Empty;

        public int? ProductId { get; set; }

        public Product? Product { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductCode { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string? Recipe { get; set; }

        public bool IsPrinted { get; set; } = false;

        public DateTime? PrintedDate { get; set; }

        public DateTime? StartDate { get; set; }

        public string? CheckedBy { get; set; }

        public DateTime? FinishedDate { get; set; }

        public string? Notes { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}