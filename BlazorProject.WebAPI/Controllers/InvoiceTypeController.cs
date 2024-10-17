using BlazorProject.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceTypeController : ControllerBase
    {
        private readonly IInvoiceTypeRepository _invoiceTypeRepository;
        public InvoiceTypeController(IInvoiceTypeRepository invoiceTypeRepository)
        {
            _invoiceTypeRepository = invoiceTypeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetInvoiceTypes() 
        {
            var invoiceTypes = await _invoiceTypeRepository.GettAllInvoiceTypesAsync();
            return Ok(invoiceTypes);
        }
    }
}
