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
    public class LocationController : GenericController<Location>
    {
        private readonly IGenericRepositoryInterface<Location> _genericRepository;
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataAccessService _dataAccessService;

        public LocationController(IGenericRepositoryInterface<Location> genericRepositoryInterface, AppDbContext context, IHttpContextAccessor contextAccessor, DataAccessService dataAccessService)
            : base(genericRepositoryInterface)
        {
            _genericRepository = genericRepositoryInterface;
            this.context = context;
            _httpContextAccessor = contextAccessor;
            _dataAccessService = dataAccessService;
        }

        [HttpGet("GetLocationByName")]
        public async Task<Location> GetLocationByName(string name)
        {
            return await context.Locations.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
