using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorProject.Domain.Entities;
using BlazorProject.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorProject.Infrastructure.Repositories
{
    public class InvoiceTypeRepository : IInvoiceTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<InvoiceType>> GettAllInvoiceTypesAsync()
        {
            return await _context.InvoiceTypes.ToListAsync();
        }
    }
}
