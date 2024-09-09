using Server.Data;
using SHARED.Contracts;
using SHARED.DTOs;
using SHARED.Models;
using static SHARED.DTOs.ServiceResponses;
using System;
using Microsoft.EntityFrameworkCore;

namespace Server.Repositories
{
    public class ProductRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Product>
    {
        public async Task<ServiceResponses.GeneralApiResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Product.FindAsync(id);
            if (dep is null) return NotFound();

            appDbContext.Product.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<Product>> GetAll() => await appDbContext.Product.ToListAsync();

        public async Task<Product> GetById(int id) => await appDbContext.Product.FindAsync(id);

        public async Task<ServiceResponses.GeneralApiResponse> Insert(Product item)
        {
            var checkIfNull = await CheckName(item.Name);
            if (!checkIfNull)
                return new GeneralApiResponse(false, "Product already added");
            appDbContext.Product.Add(item);
            await Commit();
            return Success();
        }

        public async Task<ServiceResponses.GeneralApiResponse> Update(Product item)
        {
            var dep = await appDbContext.Product.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            dep.Productid = item.Productid;
            dep.quantity_in_stock = item.quantity_in_stock;
            dep.Category_id = item.Category_id;
            dep.color = item.color;
            dep.width = item.width;
            dep.invoice_No = item.invoice_No;
            dep.density = item.density;
            dep.Supplier_Id = item.Supplier_Id;
            dep.date = item.date;
            dep.Location_id = item.Location_id;
            await Commit();
            return Success();
        }

        private static GeneralApiResponse NotFound() => new(false, "Sorry Product not found");
        private static GeneralApiResponse Success() => new(true, "Process completed");
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Product.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
