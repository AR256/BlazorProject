using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorProject.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(Guid id)
        {
            var invoice = await GetInvoiceByIdAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Invoice> GetInvoiceByIdAsync(Guid id)
        {
            return await _context.Invoices
                                 .Include(i => i.InvoiceItems)
                                 .ThenInclude(t => t.InvoiceItemTaxes)
                                 .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Invoice>> GettAllInvoicesAsync()
        {
            return await _context.Invoices
                                 .Include(i => i.Customer)
                                 .Include(i => i.InvoiceItems)
                                 .ThenInclude(t => t.InvoiceItemTaxes)
                                 .ToListAsync();
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void AddInvoiceItem(InvoiceItem newItem)
        {
            _context.InvoiceItems.Add(newItem);
        }
        public void AddInvoiceItemTax(InvoiceItemTax newTax)
        {
            _context.InvoiceItemTaxes.Add(newTax);
        }
        public void RemoveInvoiceItemTax(InvoiceItemTax Tax)
        {
            _context.InvoiceItemTaxes.Remove(Tax);
        }
        public void RemoveInvoiceItem(InvoiceItem Item)
        {
            _context.InvoiceItems.Remove(Item);
        }
    }
}
