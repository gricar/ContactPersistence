namespace ContactPersistence.Models
{
    public class Contact
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public int DddCode { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
