using ISO_ERP.Models.Enums;

namespace ISO_ERP.Models
{
    public class Detail
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Notes { get; set; }
        
        public DetailType Type { get; set; } = DetailType.Normal;

        public bool IsActive { get; set; } = true;

        public List<ProductDetail> ProductDetails { get; set; } = new();
    }
}