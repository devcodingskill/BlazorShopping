﻿using Shopping.Models.Dtos;
using Shopping.Web.Services.Contracts;
using System.Net.Http.Json;

namespace Shopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
                return products;
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"Error fetching products: {httpRequestException.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<ProductDto> GetItem(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Product/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(ProductDto);
                    else
                    {
                        var productDto = await response.Content.ReadFromJsonAsync<ProductDto>();
                        return productDto;
                    }
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();                    
                    throw new Exception(message);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"Error fetching products: {httpRequestException.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
