using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorProject.Domain.Entities;

namespace BlazorProject.Domain.Interfaces
{
    public interface IInvoiceTypeRepository
    {
        Task<List<InvoiceType>> GettAllInvoiceTypesAsync();
    }
}
