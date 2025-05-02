using Api.GRRInnovations.SmartInventory.Domain.Products;
using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Api.GRRInnovations.SmartInventory.Interfaces.Repositories;
using EFCore.BulkExtensions;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;

namespace Api.GRRInnovations.SmartInventory.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }

        public async Task<IProductModel> CreateAsync(IProductModel dto)
        {
            if (dto is not ProductModel productM) return null;

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                Console.WriteLine($"Entity Name: {entry.Entity.GetType().Name} - State: {entry.State}");
            }

            await _context.Products.AddAsync(productM).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return productM;
        }

        public async Task<List<IProductModel>> BulkInsertProductsAsync(List<IProductModel> dtos)
        {
            //todo: resolver o bug
            await _context.BulkInsertAsync(dtos, new BulkConfig
            {
                SqlBulkCopyOptions =  Microsoft.Data.SqlClient.SqlBulkCopyOptions.Default,
                BulkCopyTimeout = 200
            });
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return dtos;
        }

        public async Task<List<IProductModel>> GetAllAsync(ProductOptionsPagination productOptions)
        {
            return await Query(productOptions).ToListAsync<IProductModel>();
        }

        public async Task<List<IProductModel>> GetAllSplitQueryAsync(ProductOptionsPagination productOptions)
        {
            var products = _context.Products
                    .AsSplitQuery()
                    .Include(p => p.DbCategory)
                    .Include(p => p.DbStockEntries)
                    .Include(p => p.DbStockOutputs);

            return await products.ToListAsync<IProductModel>();
        }

        public async Task<IProductModel> GetByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Uid == id);
        }

        private IQueryable<ProductModel> Query(ProductOptionsPagination options)
        {
            var query = _context.Products.AsQueryable();

            query.Cacheable();

            if (options.AsNoTracking) query = query.AsNoTracking();

            if (options.FilterNames != null) query = query.Where(p => options.FilterNames.Contains(p.Name));

            query = options.OrderBy switch
            {
                EOrderByType.UnitPrice => options.OrderDirection == EOrderByDirection.Descending ? query.OrderByDescending(p => p.UnitPrice) : query.OrderBy(p => p.UnitPrice),
                EOrderByType.StockQuantity => options.OrderDirection == EOrderByDirection.Descending ? query.OrderByDescending(p => p.StockQuantity) : query.OrderBy(p => p.StockQuantity),
                _ => options.OrderDirection == EOrderByDirection.Descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name)
            };

            var skip = (options.Page - 1) * options.PageSize;
            var paged = query
                .Skip(skip)
                .Take(options.PageSize);

            return paged;
        }
    }
}
