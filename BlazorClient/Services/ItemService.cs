using BlazorClient.Models;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public class ItemService
    {
        private readonly HttpClient _httpClient;
        public ItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Item>> GetItemsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Item>>("api/Item");
        }
        public async Task<Item> GetItemByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Item>($"api/Item/{id}");
        }
        public async Task AddItemAsync(Item newItem)
        {
            await _httpClient.PostAsJsonAsync("api/Item", newItem);
        }
        public async Task UpdateItemAsync(Item updatedItem)
        {
            await _httpClient.PutAsJsonAsync($"api/Item/{updatedItem.Id}", updatedItem);
        }
        public async Task DeleteItemAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Item/{id}");
        }
    }
}
