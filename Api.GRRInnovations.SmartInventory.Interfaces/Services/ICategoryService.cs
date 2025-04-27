using Api.GRRInnovations.SmartInventory.Interfaces.Entities;

namespace Api.GRRInnovations.SmartInventory.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<ICategoryModel>> GetAllAsync();
        Task<ICategoryModel> GetByIdAsync(Guid id);
        Task<ICategoryModel> CreateAsync(ICategoryModel dto);
        Task UpdateAsync(Guid id, ICategoryModel dto);
        Task DeleteAsync(Guid id);
    }
}
