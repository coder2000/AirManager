namespace AirManager.Infrastructure.Models
{
    public class Player
    {
        public int Id { get; set; }

        public int AirlineId { get; set; }
        public virtual Airline Airline { get; set; }
    }
}
