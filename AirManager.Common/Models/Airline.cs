namespace AirManager.Infrastructure.Models
{
    public class Airline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ceo { get; set; }

        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
