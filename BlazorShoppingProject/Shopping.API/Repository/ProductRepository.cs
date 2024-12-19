using Microsoft.EntityFrameworkCore;
using Shopping.API.Data;
using Shopping.API.Entities;
using Shopping.API.Repository.Contacts;

namespace Shopping.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDBContext _shopOnlineDBContext;

        public ProductRepository(ShopOnlineDBContext shopOnlineDBContext)
        {
         _shopOnlineDBContext = shopOnlineDBContext;   
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await _shopOnlineDBContext.ProductCategories.ToListAsync();
            return categories;
        }

        public Task<ProductCategory> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await _shopOnlineDBContext.Products.ToListAsync();
            return products;
        }

        public Task<Product> GetItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
