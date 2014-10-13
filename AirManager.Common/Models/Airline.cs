using System;
using System.Collections.Generic;

namespace AirManager.Infrastructure.Models
{
    public enum AirlineMarket
    {
        Regional,
        Global
    }

    public class Airline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ceo { get; set; }
        public string Iata { get; set; }
        public AirlineMarket Market { get; set; }
        public Boolean Real { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual List<Airliner> Airliners { get; set; }
    }
}
