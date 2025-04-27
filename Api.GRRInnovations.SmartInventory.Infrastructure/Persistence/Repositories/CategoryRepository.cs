using Api.GRRInnovations.SmartInventory.Domain.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.GRRInnovations.SmartInventory.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICategoryModel> CreateAsync(ICategoryModel dto)
        {
            if (dto is not CategoryModel categoryM) return null;

            await _context.Categories.AddAsync(categoryM).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return categoryM;
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null) throw new KeyNotFoundException("Category not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ICategoryModel>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync<ICategoryModel>();
        }

        public async Task<ICategoryModel?> GetByIdAsync(Guid id)
        {
            return await _context
                .Categories
                .FirstOrDefaultAsync(x => x.Uid == id);
        }

        public Task UpdateAsync(Guid id, ICategoryModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
