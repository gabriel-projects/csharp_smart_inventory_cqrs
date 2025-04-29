using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;

namespace Api.GRRInnovations.SmartInventory.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<IProductModel>> GetAllAsync(ProductOptionsPagination productOptions);
        Task<IProductModel> GetByIdAsync(Guid id);
        Task<IProductModel> CreateAsync(IProductModel dto);
    }
}
