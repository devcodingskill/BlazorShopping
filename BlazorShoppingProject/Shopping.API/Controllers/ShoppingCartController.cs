using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Extensions;
using Shopping.API.Repository.Contacts;
using Shopping.Models.Dtos;

namespace Shopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;

        }
        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                var cartItems = await _shoppingCartRepository.GetItems(userId);

                if (cartItems == null)
                {
                    return NoContent();
                }

                var products = await _productRepository.GetItems();

                if (products == null)
                {
                    throw new Exception("No products found in the system");
                }

                var cartItemDto = cartItems.ConvertToDto(products);
                return Ok(cartItemDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.GetItem(id);
                if (cartItem == null)
                {
                    return NotFound();
                }
                var product = await _productRepository.GetItem(cartItem.ProductId);
                if (product == null)
                {
                    return NotFound();
                }
                var item = cartItem.ConvertToDto(product);
                return Ok(item);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddItem([FromBodyAttribute] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var product = await _productRepository.GetItem(cartItemToAddDto.ProductId);
                if (product == null)
                {
                    return NotFound();
                }
                var cartItem = await _shoppingCartRepository.AddItem(cartItemToAddDto);
                if (cartItem == null)
                {
                    return BadRequest();
                }
                //standard way of returning the newly created item by returning the location of the item and the item itself using CreatedAtAction
                var item = cartItem.ConvertToDto(product);
                return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.DeleteItem(id);
                if (cartItem == null)
                {
                    return NotFound();
                }
                var product = await _productRepository.GetItem(cartItem.ProductId);
                if (product == null)
                {
                    return NotFound();
                }
                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CartItemDto>> UpdateItem(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.UpdateQty(id, cartItemQtyUpdateDto);
                if (cartItem == null)
                {
                    return NotFound();
                }
                var product = await _productRepository.GetItem(cartItem.ProductId);

                var cartItemDtos = cartItem.ConvertToDto(product); 
                
                return Ok(cartItemDtos);
            }
            catch (Exception ex)
            {

                  return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
