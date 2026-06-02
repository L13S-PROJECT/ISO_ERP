namespace ISO_ERP.Models
{
    public class Inspector
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}