using Shopping.Models.Dtos;

namespace Shopping.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(int userId);      
        Task<CartItemDto> AddItem(CartItemToAddDto item);
        Task<CartItemDto> DeleteItem(int id);
        Task<CartItemDto> UpdateItem(CartItemQtyUpdateDto item);

        //Note: that action is a delegate which does not return a value and can be used to encapsulate a method with a single parameter.
        //So any method that subscribes to this event must have a single parameter of type int.
        event Action<int> OnShoppingCartChange;

        void RaiseEventOnShoppingCartChanged(int totalQty);
    }
}
