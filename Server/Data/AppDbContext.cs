using Microsoft.EntityFrameworkCore;
using SHARED.Models;

namespace Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Checkinout>(entity =>
            entity.HasOne(d => d.Product).WithMany(p => p.Checkinout)
                .HasForeignKey(d => d.Productid)
            );

            modelBuilder.Entity<Product>(entity =>
            entity.HasOne(d => d.Categories).WithMany(p => p.Product)
                .HasForeignKey(d => d.Category_id)
            );

            modelBuilder.Entity<Checkinout>(entity =>
            entity.HasOne(d => d.Location).WithMany(p => p.Checkinout)
                .HasForeignKey(d => d.Location_id)
            );

            modelBuilder.Entity<Product>(entity =>
            entity.HasOne(d => d.Location).WithMany(p => p.Product)
                .HasForeignKey(d => d.Location_id)
            );


            modelBuilder.Entity<Product>(entity =>
            entity.HasOne(d => d.Supplier).WithMany(p => p.Product)
                .HasForeignKey(d => d.Supplier_Id)
            );
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Checkinout> Checkinout { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
