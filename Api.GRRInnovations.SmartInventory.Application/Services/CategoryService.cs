using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using Api.GRRInnovations.SmartInventory.Interfaces.Services;

namespace Api.GRRInnovations.SmartInventory.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ICategoryModel> CreateAsync(ICategoryModel dto)
        {
           return await _categoryRepository.CreateAsync(dto);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ICategoryModel>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<ICategoryModel> GetByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Guid id, ICategoryModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
