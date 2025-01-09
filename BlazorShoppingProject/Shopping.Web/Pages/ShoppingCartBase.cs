using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shopping.Models.Dtos;
using Shopping.Web.Services.Contracts;

namespace Shopping.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }


        public List<CartItemDto> ShoppingCartItems { get; set; }
        public string Errormessage { get; set; }
        protected string TotalPrice { get; set; }
        protected int TotalItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCode.UserId);
                CalculateCartSummaryTotals();
            }
            catch (Exception ex)
            {
                Errormessage = ex.Message;
            }
        }
        protected async Task DeleteCartItme_Click(int id)
        {
            try
            {
                //This method is used to delete item from the server side
                var result = await ShoppingCartService.DeleteItem(id);

                //then we remove the item from the list on the client side
                RemoveItem(id);
                CalculateCartSummaryTotals();
            }
            catch (Exception ex)
            {
                Errormessage = ex.Message;
            }
        }
        private void RemoveItem(int id)
        {
            //as we are storing the cart items in the list on the client side so we need to remove the item from the list
            //This method is used to remove item from the list on the client side
            var cartItemDto = GetCartItems(id);

            ShoppingCartItems.Remove(cartItemDto);

        }
        private CartItemDto GetCartItems(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }
        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Quantity = qty
                    };
                    var result = await ShoppingCartService.UpdateItem(updateItemDto);
                    UpdateItmeTotalPrice(result);
                    CalculateCartSummaryTotals();
                    await MakeUpdateQtyButtonVisible(id,false);
                }
                else
                {
                    var item = ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                    if (item != null)
                    {
                        item.Quantity = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetTotalPrice()
        {
            //This method is used to calculate the total price of the items in the cart
            //this will sum the price column of all the items in the cart
            TotalPrice = ShoppingCartItems.Sum(i => i.TotalPrice).ToString("C");
        }
        private void SetTotalQuantity()
        {
            //This method is used to calculate the total quantity of the items in the cart
            //this will sum the quantity column of all the items in the cart
            TotalItems = ShoppingCartItems.Sum(i => i.Quantity);
        }
        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }
        private void UpdateItmeTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItems(cartItemDto.Id);
            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Quantity;
            }
        }
        protected async Task UpdateQty_Input(int id)
        {
            await MakeUpdateQtyButtonVisible(id,true);
        }
        private async Task MakeUpdateQtyButtonVisible(int id, bool isVisible)
        {
            await JsRuntime.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, isVisible);
        }
    }
}
