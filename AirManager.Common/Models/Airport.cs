namespace AirManager.Infrastructure.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Altitude { get; set; }
        public float Offset { get; set; }
        public string DST { get; set; }
        public string TimeZone { get; set; }

        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
