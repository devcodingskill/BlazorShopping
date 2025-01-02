using Microsoft.EntityFrameworkCore;
using Shopping.API.Data;
using Shopping.API.Entities;
using Shopping.API.Repository.Contacts;
using Shopping.Models.Dtos;

namespace Shopping.API.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDBContext _shopOnlineDBContext;
        public ShoppingCartRepository(ShopOnlineDBContext shopOnlineDBContext)
        {
            _shopOnlineDBContext = shopOnlineDBContext;
        }
        private async Task<bool> CartItemExist(int cartId, int productId)
        {
            //check if the cart item exist in the cart
            return await this._shopOnlineDBContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            //check if the cart item exist in the cart
            if (!await CartItemExist(cartItemToAddDto.CartId, cartItemToAddDto.ProductId))
            {
                //when user add a product to cart. 
                //as passing parameter so we just check with product id and cart id then create a new cart item
                var item = await (from product in this._shopOnlineDBContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Quantity = cartItemToAddDto.Quantity,
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await this._shopOnlineDBContext.CartItems.AddAsync(item);
                    await this._shopOnlineDBContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;

        }


        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this._shopOnlineDBContext.Carts
                          join cartItem in this._shopOnlineDBContext.CartItems
                          on cart.Id equals cartItem.Id
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              CartId = cartItem.CartId,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity
                          }).SingleOrDefaultAsync();

            //var result = _shopOnlineDBContext.Carts.Join(_shopOnlineDBContext.CartItems,
            //               cart => cart.Id,
            //               cartItem => cartItem.Id,
            //               (cart, cartItem) => new { cart, cartItem });

            //var filteredResult = result.Where(joined => joined.cartItem.Id == id);
            //var projectedResult = filteredResult.Select(joined => new CartItem
            //{
            //    Id = joined.cartItem.Id,
            //    CartId = joined.cartItem.CartId,
            //    ProductId = joined.cartItem.ProductId,
            //    Quantity = joined.cartItem.Quantity
            //});

            //return await projectedResult.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            //return await (from cart in this._shopOnlineDBContext.Carts
            //              join cartItem in this._shopOnlineDBContext.CartItems
            //              on cart.Id equals cartItem.CartId
            //              where cart.UserId == userId
            //              select new CartItem
            //              {
            //                  Id = cartItem.Id,
            //                  CartId = cartItem.CartId,
            //                  ProductId = cartItem.ProductId,
            //                  Quantity = cartItem.Quantity
            //              }).ToListAsync();

            return await _shopOnlineDBContext.Carts
                .Where(c => c.UserId == userId)
               .Join(_shopOnlineDBContext.CartItems, c => c.Id, ci => ci.CartId,
               (c, ci) => new CartItem
               {
                   Id = ci.Id,
                   CartId = ci.CartId,
                   ProductId = ci.ProductId,
                   Quantity = ci.Quantity
               }).ToListAsync();


        }

        public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemToUpdateDto)
        {
            throw new NotImplementedException();
        }
        public async Task<CartItem> DeleteItem(int id)
        {
            var cartItem = await _shopOnlineDBContext.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _shopOnlineDBContext.CartItems.Remove(cartItem);
                await this._shopOnlineDBContext.SaveChangesAsync();

            }
            return cartItem;
        }
    }
}
