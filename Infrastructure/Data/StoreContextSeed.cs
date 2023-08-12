using Core.Entities;
using Core.Entities.OrderAggregate;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); //has some line of code included in Infrastructure.csproj

            if (!context.Products.Any())
            {

                //var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");//We cant hv this type of file path in production. This path is for your local system
                var productsData = File.ReadAllText(path + @"/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products == null)
                    return;

                context.Products.AddRange(products);
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText(path + @"/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types == null)
                    return;

                context.ProductTypes.AddRange(types);
            }

            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText(path + @"/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands == null)
                    return;

                context.ProductBrands.AddRange(brands);
            }
            if (!context.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText(path + @"/Data/SeedData/delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (methods == null)
                    return;

                context.DeliveryMethods.AddRange(methods);
            }

            if (context.ChangeTracker.HasChanges())  //this means that if there is any changes from the data, save the changes in the db.
                await context.SaveChangesAsync();
        }

    }
}






