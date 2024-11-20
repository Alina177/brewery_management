using System.Collections.Generic;

namespace BreweryManagement.Models
{
    public class Wholesaler
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<WholesalerStock> WholesalerStocks { get; set; }
    }
}


