using Shopping.Models.Dtos;

namespace Shopping.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
        Task<ProductDto> GetItem(int id);
        Task<IEnumerable<ProductCategoryDto>> GetProductCategories();
        Task<IEnumerable<ProductDto>> GetItemsByCategory(int id);
        //Task<ProductCategoryDto> GetCategory(int id);

    }
}
