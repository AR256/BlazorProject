using BlazorProject.Application.DTOS;
using BlazorProject.Application.Exceptions;
using BlazorProject.Application.Exceptions.BlazorProject.Application.Exceptions;
using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var item = new Item { Id = Guid.NewGuid(), Name = dto.Name, Code = dto.Code };
                await _itemRepository.AddAsync(item);
                return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
            }
            catch (DuplicateEntityException ex)
            {
                return BadRequest(new ApiResponse{ Message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, new ApiResponse { Message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] ItemDto dto)
        {
            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var item = await _itemRepository.GetByIdAsync(id);
                if (item == null) throw new NotFoundException($"Item with Id {id} is not found");
                item.Name = dto.Name;
                item.Code = dto.Code;
                await _itemRepository.UpdateAsync(item);
                return Ok(new ApiResponse { Message = "Item updated successfully." });
            }
            catch (DuplicateEntityException ex)
            {
                return BadRequest(new ApiResponse { Message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { Message = ex.Message });
            }
           
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null) return NotFound();
            await _itemRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
