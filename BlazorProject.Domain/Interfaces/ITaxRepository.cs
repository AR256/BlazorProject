using BlazorProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Domain.Interfaces
{
    public interface ITaxRepository
    {
        Task AddAsync(Tax tax);
        Task<Tax> GetByIdAsync(Guid id);
        Task<IEnumerable<Tax>> GetAllAsync();
        Task UpdateAsync(Tax tax);
        Task DeleteAsync(Guid id);
    }
}
