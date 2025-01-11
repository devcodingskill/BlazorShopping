using Shopping.API.Entities;

namespace Shopping.API.Repository.Contacts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories(); 
        Task<ProductCategory> GetCategory(int id);
        Task<Product> GetItem(int id);
        Task<IEnumerable<Product>> GetItemsByCategory(int id);
    }
}
