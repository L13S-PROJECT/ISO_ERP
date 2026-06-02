namespace ISO_ERP.Models
{
    public class ProductionItem
    {
        public int Id { get; set; }

        public int ProductionId { get; set; }

        public Production? Production { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }
    }
}
