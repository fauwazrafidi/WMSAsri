using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Service;
using SHARED.Contracts;
using SHARED.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : GenericController<Categories>
    {
        private readonly IGenericRepositoryInterface<Categories> _genericRepository;
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataAccessService _dataAccessService;

        public CategoriesController(IGenericRepositoryInterface<Categories> genericRepositoryInterface, AppDbContext context, IHttpContextAccessor contextAccessor, DataAccessService dataAccessService)
            : base(genericRepositoryInterface)
        {
            _genericRepository = genericRepositoryInterface;
            this.context = context;
            _httpContextAccessor = contextAccessor;
            _dataAccessService = dataAccessService;
        }


    }
}
