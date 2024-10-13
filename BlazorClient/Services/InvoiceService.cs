using BlazorClient.Models;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public class InvoiceService
    {
        private readonly HttpClient _httpClient;
        public InvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Invoice>>("api/Invoice");
        }
        public async Task<Invoice> GetInvoiceyIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Invoice>($"api/Invoice/{id}");
        }
        public async Task AddInvoiceAsync(Invoice newInvoice)
        {
            await _httpClient.PostAsJsonAsync("api/Invoice", newInvoice);
        }
        public async Task UpdateInvoiceAsync(Invoice updatedInvoice)
        {
            await _httpClient.PutAsJsonAsync($"api/Invoice/{updatedInvoice.Id}", updatedInvoice);
        }
        public async Task DeleteInvoiceAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Invoice/{id}");
        }
    }
}
