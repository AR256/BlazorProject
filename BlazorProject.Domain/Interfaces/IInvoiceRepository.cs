using BlazorProject.Domain.Entities;

namespace BlazorProject.Domain.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GettAllInvoicesAsync();
        Task<Invoice> GetInvoiceByIdAsync(Guid id);
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(Guid id);
        Task SaveChangesAsync();
        void AddInvoiceItemTax(InvoiceItemTax newTax);
        void RemoveInvoiceItemTax(InvoiceItemTax Tax);
        void AddInvoiceItem(InvoiceItem newItem);
        void RemoveInvoiceItem(InvoiceItem item);
    }
}
