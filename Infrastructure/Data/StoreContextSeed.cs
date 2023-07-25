using Core.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            // var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.Products.Any())
            {

                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products == null)
                    return;

                context.Products.AddRange(products);
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types == null)
                    return;

                context.ProductTypes.AddRange(types);
            }

            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands == null)
                    return;

                context.ProductBrands.AddRange(brands);
            }

            if (context.ChangeTracker.HasChanges())  //this means that if there is any changes from the data, save the changes in the db.
                await context.SaveChangesAsync();
        }

    }
}






