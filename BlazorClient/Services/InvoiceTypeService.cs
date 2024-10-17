using System.Net.Http.Json;
using BlazorClient.Models;

namespace BlazorClient.Services
{
    public class InvoiceTypeService
    {
        private readonly HttpClient _httpClient;
        public InvoiceTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<InvoiceType>> GetInvoiceTypesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<InvoiceType>>("api/InvoiceType");
        }
    }
}
