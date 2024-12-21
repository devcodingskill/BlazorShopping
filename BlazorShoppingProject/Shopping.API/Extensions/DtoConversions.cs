using Shopping.API.Entities;
using Shopping.Models.Dtos;

namespace Shopping.API.Extensions
{
    public static class DtoConversions
    {
        //convert Product to ProductDto
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products, IEnumerable<ProductCategory> productCategories)
        {

            // Log the contents of products and productCategories
            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.Id}, CategoryId: {product.CategoryId}");
            }

            Console.WriteLine("ProductCategories:");
            foreach (var category in productCategories)
            {
                Console.WriteLine($"Id: {category.Id}");
            }

            // Create a list of ProductDto and add dummy data
            //var productDtos = new List<ProductDto>
            //{
            //    new ProductDto
            //    {
            //        Id = 1,
            //        Name = "Dummy Product 1",
            //        Description = "Description for Dummy Product 1",
            //        Price = 10.99m,
            //        ImageURL = "http://example.com/dummy1.jpg",
            //        Quantity = 5,
            //        CategoryId = 1,
            //        CategoryName = "Dummy Category 1"
            //    },
            //    new ProductDto
            //    {
            //        Id = 2,
            //        Name = "Dummy Product 2",
            //        Description = "Description for Dummy Product 2",
            //        Price = 20.99m,
            //        ImageURL = "http://example.com/dummy2.jpg",
            //        Quantity = 10,
            //        CategoryId = 2,
            //        CategoryName = "Dummy Category 2"
            //    }
            //};



            //join the product and product category tables to get the category name
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
        public static ProductDto ConvertToDto(this Product product, ProductCategory productCategory)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageURL = product.ImageURL,
                Quantity = product.Qty,
                CategoryId = product.CategoryId,
                CategoryName = productCategory.Name
            };
        }

    }
}
