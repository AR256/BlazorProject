using BlazorProject.Application.DTOS;
using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;
using BlazorProject.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxRepository _taxRepository;

        public TaxController(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTaxes()
        {
            var taxes = await _taxRepository.GetAllAsync();
            return Ok(taxes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaxById(Guid id)
        {
            var tax = await _taxRepository.GetByIdAsync(id);
            if (tax == null)
                return NotFound();

            return Ok(tax);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTax([FromBody] TaxDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tax = new Tax { Id = Guid.NewGuid(), Name = dto.Name, Code = dto.Code };
            await _taxRepository.AddAsync(tax);
            return CreatedAtAction(nameof(GetTaxById), new { id = tax.Id }, tax);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTax(Guid id, [FromBody] TaxDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tax = await _taxRepository.GetByIdAsync(id);
            if (tax == null)
                return NotFound();
            tax.Name = dto.Name;
            tax.Code = dto.Code;
            await _taxRepository.UpdateAsync(tax);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTax(Guid id)
        {
            var tax = await _taxRepository.GetByIdAsync(id);
            if (tax == null)
                return NotFound();

            await _taxRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
