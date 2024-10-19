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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(i => i.Code == customer.Code);
            if (existingCustomer != null)
            {
                throw new DuplicateEntityException($"A customer with the code {customer.Code} already exists.");
            }
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await GetByIdAsync(id);
            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer), id);
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(i => i.Code == customer.Code && i.Id != customer.Id);

            if (existingCustomer != null)
            {
                throw new DuplicateEntityException($"A tax with the code {customer.Code} already exists.");
            }
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
