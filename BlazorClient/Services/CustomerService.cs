using BlazorClient.Models;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customer");
        }
        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Customer>($"api/Customer/{id}");
        }
        public async Task AddCustomerAsync(Customer newCustomer)
        {
            await _httpClient.PostAsJsonAsync("api/Customer", newCustomer);
        }
        public async Task UpdateCustomerAsync(Customer updatedCustomer)
        {
            await _httpClient.PutAsJsonAsync($"api/Customer/{updatedCustomer.Id}", updatedCustomer);
        }
        public async Task DeleteCustomerAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Customer/{id}");
        }
    }
}
