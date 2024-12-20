using Microsoft.AspNetCore.Components;
using Shopping.Models.Dtos;
using Shopping.Web.Services.Contracts;

namespace Shopping.Web.Pages
{
    //This class need to be inherited by other components
    public class ProductsBase : ComponentBase
    {
        //Inject the ProductService to get the products
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        //We need to override the OnInitializedAsync method to get the products
        //this is a lifecycle method for blazor that is called when the component is initialized
        override protected async Task OnInitializedAsync()
        {
            Products = await ProductService.GetItems();
        }
    }
}
