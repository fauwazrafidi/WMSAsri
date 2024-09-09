using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Service;
using SHARED.Contracts;
using SHARED.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : GenericController<Product>
    {
        private readonly IGenericRepositoryInterface<Product> _genericRepository;
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataAccessService _dataAccessService;

        public ProductController(IGenericRepositoryInterface<Product> genericRepositoryInterface, AppDbContext context, IHttpContextAccessor contextAccessor, DataAccessService dataAccessService)
            : base(genericRepositoryInterface)
        {
            _genericRepository = genericRepositoryInterface;
            this.context = context;
            _httpContextAccessor = contextAccessor;
            _dataAccessService = dataAccessService;
        }

        [HttpGet("GetProductByName")]
        public IActionResult GetProductByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Search term cannot be empty.");
            }

            // Convert the search term to lower case for case-insensitive search
            string searchTermLower = name.ToLower();

            // Search for RawMaterialData where REMARK2 or ITEMCODE contains the search term (case-insensitive)
            var productData = _dataAccessService.GetProductData()
                                                    .Where(data =>
                                                        (data.Name?.ToLower().Contains(searchTermLower, StringComparison.CurrentCultureIgnoreCase) ?? false))
                                                    .ToList();

            if (productData.Count == 0)
            {
                return NotFound("No matching data found.");
            }

            return Ok(productData);


        }

        [HttpGet("GetProductByProductId")]
        public async Task<Product> GetProductByProductId(string productid)
        {
            return await context.Product.FirstOrDefaultAsync(x => x.Productid == productid);
        }


    }
}
