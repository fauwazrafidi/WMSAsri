using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class AuthDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
    }
}
