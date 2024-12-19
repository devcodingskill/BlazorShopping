using Shopping.API.Entities;
using Shopping.Models.Dtos;

namespace Shopping.API.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products, IEnumerable<ProductCategory> productCategories)
        {
            var productDtos = (from product in products
                               join productCategory in productCategories
                               on product.CategoryId equals productCategory.Id
                               select new ProductDto
                               {
                                   Id = product.Id,
                                   Name = product.Name,
                                   Description = product.Description,
                                   Price = product.Price,
                                   ImageURL = product.ImageURL,
                                   Quantity = product.Qty,
                                   CategoryId = product.CategoryId,
                                   CategoryName = productCategory.Name
                               }).ToList();

            return productDtos;            
        }
    }
}
