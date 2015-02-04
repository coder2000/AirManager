// Copyright 2014 Dieter Lunn All Rights Reserved

namespace AirManager.Infrastructure.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string TimeZone { get; set; }

        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
