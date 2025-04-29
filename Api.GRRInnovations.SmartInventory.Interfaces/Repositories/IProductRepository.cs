using Api.GRRInnovations.SmartInventory.Interfaces.Entities;

namespace Api.GRRInnovations.SmartInventory.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<IProductModel>> GetAllAsync(ProductOptionsPagination productOptions);
        Task<IProductModel> GetByIdAsync(Guid id);
        Task<IProductModel> CreateAsync(IProductModel dto);
    }

    public class ProductOptionsBase
    {
        public List<Guid> FilterUids { get; set; }
        public List<string> FilterNames { get; set; }
        public List<Guid> FilterCategoriesUids { get; set; }
        public List<Guid> FilterSupplierUids { get; set; }
        public bool IncludeCategory { get; set; }
        public bool IncludeSupplier { get; set; }
        public bool AsNoTracking { get; set; }
    }

    public class ProductOptionsPagination : ProductOptionsBase
    {
        public EOrderByType OrderBy { get; set; } = EOrderByType.Name;
        public EOrderByDirection OrderDirection { get; set; } = EOrderByDirection.Ascending;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
