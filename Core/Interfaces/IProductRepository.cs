using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();  //you can only read from the db, you cant add to it
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}
