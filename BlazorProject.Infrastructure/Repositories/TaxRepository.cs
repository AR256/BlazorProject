using BlazorProject.Application.Exceptions;
using BlazorProject.Application.Exceptions.BlazorProject.Application.Exceptions;
using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Infrastructure.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        private readonly ApplicationDbContext _context;
        public TaxRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Tax tax)
        {
            var existingTax = await _context.Taxes.FirstOrDefaultAsync(i => i.Code == tax.Code);
            if (existingTax != null)
            {
                throw new DuplicateEntityException($"A tax with the code {tax.Code} already exists.");
            }
            _context.Taxes.Add(tax);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tax = await GetByIdAsync(id);
            if (tax == null)
            {
                throw new NotFoundException(nameof(Tax), id);
            }

            _context.Taxes.Remove(tax);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tax>> GetAllAsync()
        {
            return await _context.Taxes.ToListAsync();
        }

        public async Task<Tax> GetByIdAsync(Guid id)
        {
            var tax = await _context.Taxes.FindAsync(id);

            return tax;
        }

        public async Task UpdateAsync(Tax tax)
        {
            var existingTax = await _context.Taxes.FirstOrDefaultAsync(i => i.Code == tax.Code && i.Id != tax.Id);

            if (existingTax != null)
            {
                throw new DuplicateEntityException($"A tax with the code {tax.Code} already exists.");
            }
            _context.Taxes.Update(tax);
            await _context.SaveChangesAsync();
        }
    }
}
