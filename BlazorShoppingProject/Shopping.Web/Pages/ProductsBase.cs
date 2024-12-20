using Microsoft.AspNetCore.Components;
using Shopping.Models.Dtos;
using Shopping.Web.Services.Contracts;

namespace Shopping.Web.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        override protected async Task OnInitializedAsync()
        {
            Products = await ProductService.GetItems();
        }
    }
}
