using Shopping.Models.Dtos;

namespace Shopping.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
        //Task<IEnumerable<ProductCategoryDto>> GetCategories();
        //Task<ProductCategoryDto> GetCategory(int id);
        //Task<ProductDto> GetItem(int id);
    }
}
