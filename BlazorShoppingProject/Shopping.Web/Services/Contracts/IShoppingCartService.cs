using Shopping.Models.Dtos;

namespace Shopping.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDto>> GetItems(int userId);      
        Task<CartItemDto> AddItem(CartItemToAddDto item);
       
    }
}
