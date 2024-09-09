using Microsoft.EntityFrameworkCore;
using Server.Data;
using SHARED.Contracts;
using SHARED.DTOs;
using SHARED.Models;
using static SHARED.DTOs.ServiceResponses;

namespace Server.Repositories
{
    public class CategoriesRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Categories>
    {
        public async Task<ServiceResponses.GeneralApiResponse> DeleteById(int id)
        {
            var item = await appDbContext.Categories.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.Categories.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<Categories>> GetAll() => await appDbContext
            .Categories
            .AsNoTracking()
            .ToListAsync();

        public async Task<Categories> GetById(int id) => await appDbContext.Categories.FindAsync(id);

        public async Task<ServiceResponses.GeneralApiResponse> Insert(Categories item)
        {
            if (!await CheckName(item.Name!))
                return new GeneralApiResponse(false, "Category Type already added");
            appDbContext.Categories.Add(item);
            await Commit();
            return Success();
        }

        public async Task<ServiceResponses.GeneralApiResponse> Update(Categories item)
        {
            var obj = await appDbContext.Categories.FindAsync(item.Id);
            if (obj is null) return NotFound();
            obj.Name = item.Name;
            obj.Description = item.Description;
            await Commit();
            return Success();
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralApiResponse NotFound() => new(false, "Sorry Category not found");
        private static GeneralApiResponse Success() => new(true, "Process completed");
        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Categories
                .FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
