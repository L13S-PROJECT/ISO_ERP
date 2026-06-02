namespace ISO_ERP.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;
    }
}