using Server.Data;
using SHARED.Models;

namespace Server.Service
{
    public class DataAccessService
    {
        private readonly AppDbContext _context;

        public DataAccessService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductData()
        {

            var productData = from p in _context.Product
                              select new Product
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  color = p.color,
                                  date = p.date,
                                  invoice_No = p.invoice_No,
                                  density = p.density,
                                  Category_id = p.Category_id,
                                  Location_id = p.Location_id,
                                  Supplier_Id = p.Supplier_Id,
                                  Productid = p.Productid,
                                  width = p.width,
                                  quantity_in_stock = p.quantity_in_stock
                              };


            //Product rawMaterialData = _context.Product.FirstOrDefaultAsync(x => x.Name == name).Result;


            return productData.ToList();
        }
    }
}
