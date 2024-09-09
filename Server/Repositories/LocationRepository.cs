using Server.Data;
using Microsoft.EntityFrameworkCore;
using SHARED.Contracts;
using SHARED.DTOs;
using SHARED.Models;
using static SHARED.DTOs.ServiceResponses;

namespace Server.Repositories
{
    public class LocationRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Location>
    {
        public async Task<ServiceResponses.GeneralApiResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Locations.FindAsync(id);
            if (dep is null) return NotFound();

            appDbContext.Locations.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<Location>> GetAll() => await appDbContext.Locations.ToListAsync();

        public async Task<Location> GetById(int id) => await appDbContext.Locations.FindAsync(id);

        public async Task<ServiceResponses.GeneralApiResponse> Insert(Location item)
        {
            var checkIfNull = await CheckName(item.Name);
            if (!checkIfNull)
                return new GeneralApiResponse(false, "Location already added");
            appDbContext.Locations.Add(item);
            await Commit();
            return Success();
        }

        public async Task<ServiceResponses.GeneralApiResponse> Update(Location item)
        {
            var dep = await appDbContext.Locations.FindAsync(item.LocationId);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            dep.Description = item.Description;
            await Commit();
            return Success();
        }

        private static GeneralApiResponse NotFound() => new(false, "Sorry Location not found");
        private static GeneralApiResponse Success() => new(true, "Process completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Locations.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
