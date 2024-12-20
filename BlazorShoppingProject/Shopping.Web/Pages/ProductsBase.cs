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
