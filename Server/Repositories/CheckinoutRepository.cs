using Microsoft.EntityFrameworkCore;
using Server.Data;
using SHARED.Contracts;
using SHARED.DTOs;
using SHARED.Models;
using static SHARED.DTOs.ServiceResponses;

namespace Server.Repositories
{
    public class CheckinoutRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Checkinout>
    {
        public async Task<ServiceResponses.GeneralApiResponse> DeleteById(int id)
        {
            var item = await appDbContext.Checkinout.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.Checkinout.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<Checkinout>> GetAll() => await appDbContext
            .Checkinout
            .AsNoTracking()
            .ToListAsync();

        public async Task<Checkinout> GetById(int id) => await appDbContext.Checkinout.FindAsync(id);

        public async Task<ServiceResponses.GeneralApiResponse> Insert(Checkinout item)
        {
            //if (!await CheckName(item.Name!))
            //    return new GeneralApiResponse(false, "Category Type already added");
            appDbContext.Checkinout.Add(item);
            await Commit();
            return Success();
        }

        public async Task<ServiceResponses.GeneralApiResponse> Update(Checkinout item)
        {
            var obj = await appDbContext.Checkinout.FindAsync(item.Id);
            if (obj is null) return NotFound();
            obj.Quantity = item.Quantity;
            await Commit();
            return Success();
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralApiResponse NotFound() => new(false, "Sorry Checkinout not found");
        private static GeneralApiResponse Success() => new(true, "Process completed");
        private async Task<bool> CheckName(int id)
        {
            var item = await appDbContext.Checkinout
                .FirstOrDefaultAsync(x => x.Id!.Equals(id));
            return item is null;
        }
    }
}
