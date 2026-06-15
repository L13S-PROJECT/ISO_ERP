namespace ISO_ERP.Models
{
    public class ProductDetailSubItem
    {
        public int Id { get; set; }

        public int ProductDetailId { get; set; }

        public ProductDetail? ProductDetail { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }
}