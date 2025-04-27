using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<ICategoryModel>> GetAllAsync();
        Task<ICategoryModel?> GetByIdAsync(Guid id);
        Task<ICategoryModel> CreateAsync(ICategoryModel dto);
        Task UpdateAsync(Guid id, ICategoryModel dto);
        Task DeleteAsync(Guid id);
    }
}
