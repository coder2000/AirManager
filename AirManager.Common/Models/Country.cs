using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirManager.Infrastructure.Models
{
    public class Country
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual List<Airline> Airlines { get; set; }
    }
}
