namespace Core.Domain
{
    public class Phone
    {
        public int? PhoneId { get; set; } = null;
        public string Number { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
