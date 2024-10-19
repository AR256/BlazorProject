using BlazorProject.Application.DTOS;
using BlazorProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;
        public InvoiceController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _invoiceService.GetInvoices();
            return Ok(invoices);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(Guid id)
        {
            var invoice = await _invoiceService.GetInvoice(id);

            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _invoiceService.CreateInvoice(invoiceDto);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(Guid id, [FromBody] InvoiceDto invoiceDto)
        {
            await _invoiceService.UpdateInvoice(id, invoiceDto);
            return AcceptedAtAction(nameof(UpdateInvoice));
        }
        [HttpDelete("{id}")]
        public async Task DeleteInvoiceAsync(Guid id)
        {
            await _invoiceService.DeleteInvoice(id);
        }
    }
}
