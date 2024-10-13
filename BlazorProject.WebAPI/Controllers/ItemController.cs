using BlazorProject.Application.DTOS;
using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemRepository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = new Item{ Id = Guid.NewGuid(), Name = dto.Name, Code = dto.Code };
            await _itemRepository.AddAsync(item);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] ItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null)
                return NotFound();
            item.Name = dto.Name;
            item.Code = dto.Code;
            await _itemRepository.UpdateAsync(item);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            await _itemRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
