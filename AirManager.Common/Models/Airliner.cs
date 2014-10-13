using System.Collections.Generic;

namespace AirManager.Infrastructure.Models
{
    public enum AirlinerType
    {
        WideBody,
        NarrowBody
    }

    public enum AirlinerRange
    {
        LongRange,
        MediumRange
    }

    public class Airliner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public AirlinerType Type { get; set; }
        public AirlinerRange Range { get; set; }
        public int PassengerCount { get; set; }

        public virtual List<Airline> Airlines { get; set; } 
    }
}
