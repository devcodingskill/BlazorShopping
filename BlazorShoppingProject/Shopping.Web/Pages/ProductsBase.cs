using Microsoft.AspNetCore.Components;
using Shopping.Models.Dtos;
using Shopping.Web.Services.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace Shopping.Web.Pages
{
    //This class need to be inherited by other components
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        //Inject the ProductService to get the products
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        //We need to override the OnInitializedAsync method to get the products
        //this is a lifecycle method for blazor that is called when the component is initialized
        override protected async Task OnInitializedAsync()
        {
            try
            {
                Products = await ProductService.GetItems();

                var shoppingCartItem = await ShoppingCartService.GetItems(HardCode.UserId);
                var totalItems = shoppingCartItem.Sum(i => i.Quantity);
                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalItems);
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupProductsByCategory()
        {
            return Products.GroupBy(p => p.CategoryId).OrderBy(g => g.Key);
        }

        protected string GetCategoryName(IGrouping<int, ProductDto> groupProductDtos)
        {
            return groupProductDtos.FirstOrDefault(pg => pg.CategoryId == groupProductDtos.Key).CategoryName;
        }
    }
}
