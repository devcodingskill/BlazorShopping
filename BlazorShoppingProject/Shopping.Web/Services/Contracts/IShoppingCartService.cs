﻿using Shopping.Models.Dtos;

namespace Shopping.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(int userId);      
        Task<CartItemDto> AddItem(CartItemToAddDto item);
        Task<CartItemDto> DeleteItem(int id);

    }
}
