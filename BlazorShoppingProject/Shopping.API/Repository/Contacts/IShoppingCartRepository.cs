using Shopping.API.Entities;
using Shopping.Models.Dtos;

namespace Shopping.API.Repository.Contacts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id ,CartItemQtyUpdateDto cartItemToUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);

    }
}
