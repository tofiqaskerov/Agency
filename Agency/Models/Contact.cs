namespace Agency.Models
{
    public class Contact : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Message { get; set; }
        public bool IsRead  { get; set; }
    }
}
