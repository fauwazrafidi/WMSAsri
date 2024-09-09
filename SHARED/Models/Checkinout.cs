using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Models
{
    public class Checkinout
    {
        public int Id { get; set; }
        public int Productid { get; set; }
        public virtual Product? Product { get; set; }
        public double Quantity { get; set; }
        public string Scandate { get; set; }
        public string Transaction_type { get; set; }

        public int? Location_id { get; set; }
        public virtual Location? Location { get; set; }
    }
}
