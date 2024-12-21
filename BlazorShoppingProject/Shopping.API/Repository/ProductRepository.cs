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

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category =await _shopOnlineDBContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await _shopOnlineDBContext.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetItem(int id)
        {
           var product = await _shopOnlineDBContext.Products.FindAsync(id);
           return product;
        }
    }
}
