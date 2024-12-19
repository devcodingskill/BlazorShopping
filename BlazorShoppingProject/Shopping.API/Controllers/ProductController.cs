using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Entities;
using Shopping.API.Extensions;
using Shopping.API.Repository.Contacts;
using Shopping.Models.Dtos;

namespace Shopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
           
            try
            {
                var products = await _productRepository.GetItems();
                var productCategories = await _productRepository.GetCategories();
                
                if (products == null || productCategories == null)
                    return NotFound();
                else
                {
                    var productDtos = products.ConvertToDto(productCategories);
                    return Ok(productDtos);
                }
            }
            catch (Exception)
            {
                return  StatusCode(StatusCodes.Status500InternalServerError,"error retrieving data from the database");
            }
            
        }
    }
}
