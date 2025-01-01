using Microsoft.AspNetCore.Components;
using Shopping.Models.Dtos;
using Shopping.Web.Services.Contracts;

namespace Shopping.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
       

        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }
       public string Errormessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCode.UserId);
            }
            catch (Exception ex)
            {
                Errormessage = ex.Message;
            }
        }
    }
}
