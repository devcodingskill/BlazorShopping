using Microsoft.AspNetCore.Components;
using Shopping.Models.Dtos;

namespace Shopping.Web.Pages
{
    public class DisplayProductBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }

    }
}
