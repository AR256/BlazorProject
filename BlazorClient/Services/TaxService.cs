using BlazorClient.Models;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public class TaxService
    {
        private readonly HttpClient _httpClient;
        public TaxService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Tax>> GetTaxesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Tax>>("api/Tax");
        }
        public async Task<Tax> GetTaxByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Tax>($"api/Tax/{id}");
        }
        public async Task<HttpResponseMessage> AddTaxAsync(Tax newTax)
        {
            return await _httpClient.PostAsJsonAsync("api/Tax", newTax);
        }
        public async Task<HttpResponseMessage> UpdateTaxAsync(Tax updatedTax)
        {
            return await _httpClient.PutAsJsonAsync($"api/Tax/{updatedTax.Id}", updatedTax);
        }
        public async Task DeleteTaxAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Tax/{id}");
        }
    }
}
