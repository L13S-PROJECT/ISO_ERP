namespace ISO_ERP.Models
{
    public class ProductDetailCreateModel
    {
        public int DetailId { get; set; }

        public int DisplayOrder { get; set; }

        public List<ProductDetailSubItemCreateModel> SubItems { get; set; } = new();
    }
}