using Microsoft.EntityFrameworkCore;
using Server.Data;
using SHARED.Contracts;
using SHARED.DTOs;
using SHARED.Models;
using static SHARED.DTOs.ServiceResponses;

namespace Server.Repositories
{
    public class SupplierRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Supplier>
    {
        public async Task<ServiceResponses.GeneralApiResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Suppliers.FindAsync(id);
            if (dep is null) return NotFound();

            appDbContext.Suppliers.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<Supplier>> GetAll() => await appDbContext.Suppliers.ToListAsync();

        public async Task<Supplier> GetById(int id) => await appDbContext.Suppliers.FindAsync(id);

        public async Task<ServiceResponses.GeneralApiResponse> Insert(Supplier item)
        {
            var checkIfNull = await CheckName(item.Name);
            if (!checkIfNull)
                return new GeneralApiResponse(false, "Supplier already added");
            appDbContext.Suppliers.Add(item);
            await Commit();
            return Success();
        }

        public async Task<ServiceResponses.GeneralApiResponse> Update(Supplier item)
        {
            var dep = await appDbContext.Suppliers.FindAsync(item.SupplierId);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            dep.ContactPerson = item.ContactPerson;
            dep.PhoneNumber = item.PhoneNumber;
            dep.Email = item.Email;
            await Commit();
            return Success();
        }

        private static GeneralApiResponse NotFound() => new(false, "Sorry Supplier not found");
        private static GeneralApiResponse Success() => new(true, "Process completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Suppliers.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
