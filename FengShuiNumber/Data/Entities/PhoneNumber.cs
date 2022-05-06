namespace FengShuiNumber.Data.Entities
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string NetworkCarrier { get; set; }
        public string? Number { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
