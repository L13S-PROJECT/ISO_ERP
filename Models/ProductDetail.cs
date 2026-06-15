namespace ISO_ERP.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int DetailId { get; set; }

        public Detail? Detail { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public List<ProductDetailSubItem> SubItems { get; set; } = new();
    }
}