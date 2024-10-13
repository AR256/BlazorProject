using BlazorProject.Application.DTOS;
using BlazorProject.Application.Exceptions.BlazorProject.Application.Exceptions;
using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;

namespace BlazorProject.Application.Services
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITaxRepository _taxRepository;
        public InvoiceService(IInvoiceRepository invoiceRepository,
                              IItemRepository itemRepository,
                              ICustomerRepository customerRepository,
                              ITaxRepository taxRepository)
        {
            _invoiceRepository = invoiceRepository;
            _itemRepository = itemRepository;
            _customerRepository = customerRepository;
            _taxRepository = taxRepository;
        }
        public async Task<ICollection<Invoice>> GetInvoices()
        {
            return await _invoiceRepository.GettAllInvoicesAsync();
        }
        public async Task<InvoiceDto> GetInvoice(Guid id)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);

            if (invoice == null)
            {
                throw new NotFoundException("Invoice Not Found");
            }
            var invoiceDto = new InvoiceDto
            {
                Id = invoice.Id,
                Type = invoice.Type,
                Code = invoice.Code,
                CustomerId = invoice.CustomerId,
                DateTimeIssued = invoice.DateTimeIssued,
                NetAmount = invoice.NetAmount,
                InvoiceItems = invoice.InvoiceItems.Select(i => new InvoiceItemDto
                {
                    Id = i.Id,
                    ItemId = i.ItemId,
                    InvoiceId = i.InvoiceId,
                    Quantity = i.Quantity,
                    Amount = i.Amount,
                    NetAmount = i.NetAmount,
                    Taxes = i.InvoiceItemTaxes.Select(t => new InvoiceItemTaxDto
                    {
                        Id = t.Id,
                        TaxId = t.TaxId,
                        Amount = t.TaxAmount
                    }).ToList()
                }).ToList()
            };
            return invoiceDto;
        }
        public async Task CreateInvoice(InvoiceDto invoiceDto)
        {
            var newInvoiceItemId = Guid.NewGuid();
            var newInvoiceId = Guid.NewGuid();

            var customer = await _customerRepository.GetByIdAsync(invoiceDto.CustomerId);
            if (customer == null) 
            {
                throw new NotFoundException(nameof(Customer), invoiceDto.CustomerId);
            }

            var invoice = new Invoice
            {
                Id = newInvoiceId,
                Type = invoiceDto.Type,
                Code = invoiceDto.Code,
                CustomerId = invoiceDto.CustomerId,
                DateTimeIssued = invoiceDto.DateTimeIssued,
                NetAmount = invoiceDto.NetAmount,
                InvoiceItems = invoiceDto.InvoiceItems.Select(i => new InvoiceItem
                {
                    Id = newInvoiceItemId,
                    ItemId = i.ItemId,
                    InvoiceId = newInvoiceId,
                    Quantity = i.Quantity,
                    Amount = i.Amount,
                    NetAmount = i.NetAmount,
                    InvoiceItemTaxes = i.Taxes.Select(t => new InvoiceItemTax
                    {
                        Id = Guid.NewGuid(),
                        TaxId = t.TaxId,
                        TaxAmount = t.Amount,
                        InvoiceItemId = newInvoiceItemId
                    }).ToList()
                }).ToList()
            };
            await _invoiceRepository.AddInvoiceAsync(invoice);
        }
        public async Task UpdateInvoice(Guid id, InvoiceDto invoiceDto)
        {
            var existingInvoice = await _invoiceRepository.GetInvoiceByIdAsync(id) ?? throw new NotFoundException();
            existingInvoice.DateTimeIssued = DateTime.Now;
            existingInvoice.CustomerId = invoiceDto.CustomerId;
            existingInvoice.Code = invoiceDto.Code;
            existingInvoice.NetAmount = invoiceDto.NetAmount;
            existingInvoice.Type = invoiceDto.Type;
            foreach(var item in existingInvoice.InvoiceItems)
            {
                _invoiceRepository.RemoveInvoiceItem(item);
            }
            foreach (var item in invoiceDto.InvoiceItems)
            {
                var itemId = Guid.NewGuid();
                var newItem = new InvoiceItem
                {
                    Id = itemId,
                    ItemId = item.ItemId,
                    Amount = item.Amount,
                    InvoiceId = item.InvoiceId,
                    InvoiceItemTaxes = item.Taxes.Select(tax => new InvoiceItemTax
                    {
                        Id = Guid.NewGuid(),
                        TaxId = tax.TaxId,
                        TaxAmount = tax.Amount,
                        InvoiceItemId = itemId,
                    }).ToList()
                };
                _invoiceRepository.AddInvoiceItem(newItem);
            }
            await _invoiceRepository.UpdateInvoiceAsync(existingInvoice);
        }
        public async Task DeleteInvoice(Guid id)
        {
            await _invoiceRepository.DeleteInvoiceAsync(id);
        }
    }
}
