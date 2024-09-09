using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Checkinout>? Checkinout { get; set; } = new List<Checkinout>();
        public virtual ICollection<Product>? Product { get; set; } = new List<Product>();
    }
}
