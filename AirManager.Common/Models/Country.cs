// Copyright 2014 Dieter Lunn All Rights Reserved

using System.Collections.Generic;

namespace AirManager.Infrastructure.Models
{
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual List<Airline> Airlines { get; set; }
        public virtual List<City> Cities { get; set; } 
    }
}
