using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Productid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Checkinout>? Checkinout { get; set; } = new List<Checkinout>();
        public int? Category_id { get; set; }
        public virtual Categories? Categories { get; set; }
        public double? quantity_in_stock { get; set; }
        public double? width { get; set; }
        public string color { get; set; }
        public string invoice_No { get; set; }
        public double? density { get; set; }
        public int? Supplier_Id { get; set; }
        public virtual Supplier? Supplier { get; set; }
        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public int? Location_id { get; set; }
        public virtual Location? Location { get; set; }
    }
}
