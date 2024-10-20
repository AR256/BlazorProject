﻿using BlazorProject.Application.Exceptions;
using BlazorProject.Application.Exceptions.BlazorProject.Application.Exceptions;
using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorProject.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Item item)
        {
            var existingItem = await _context.Items.FirstOrDefaultAsync(i => i.Code == item.Code);

            if (existingItem != null)
            {
                throw new DuplicateEntityException($"An item with the code {item.Code} already exists.");
            }
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            if (item == null)
            {
                throw new NotFoundException(nameof(Item), id);
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetByIdAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);

            return item;
        }

        public async Task UpdateAsync(Item item)
        {
            var existingItem = await _context.Items.FirstOrDefaultAsync(i => i.Code == item.Code && i.Id != item.Id);

            if (existingItem != null)
            {
                throw new DuplicateEntityException($"An item with the code {item.Code} already exists.");
            }
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
