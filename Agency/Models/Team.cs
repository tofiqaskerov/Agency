namespace Agency.Models
{
    public class Team : Base
    {
        public string PhotoURL { get; set; }
        public string Fullname { get; set; }
        public int  PositionId { get; set; }
        public Position Position { get; set; }
    }
}
