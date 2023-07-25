using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Sqlite does not support decimal type. This code will convert the decimal type to a double, which is the supported type.
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")// this is to first check if the db is Sqlite b4 initiating the conversion
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes()) //loop over our different properties
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>(); //convert to double
                    }
                }
            }
        }
    }
}
