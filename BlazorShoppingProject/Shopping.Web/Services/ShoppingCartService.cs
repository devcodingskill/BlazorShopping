﻿using Shopping.Models.Dtos;
using Shopping.Web.Services.Contracts;
using System.Net.Http.Json;

namespace Shopping.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;
        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CartItemDto> AddItem(CartItemToAddDto item)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart", item);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(CartItemDto);
                    else
                    {
                        var cartItemDto = await response.Content.ReadFromJsonAsync<CartItemDto>();
                        return cartItemDto;

                    }
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartItemDto> DeleteItem(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ShoppingCart/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return default(CartItemDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CartItemDto>> GetItems(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(List<CartItemDto>);
                    else
                    {
                        var cartItems = await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
                        return cartItems;
                    }
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status {response.StatusCode} Message - {message}");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
