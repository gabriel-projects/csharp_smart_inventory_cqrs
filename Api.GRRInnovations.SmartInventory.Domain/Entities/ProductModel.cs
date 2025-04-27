using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Api.GRRInnovations.SmartInventory.Domain.Entities
{
    public class ProductModel : BaseModel, IProductModel
    {
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public decimal UnitPrice { get; set; }

        public CategoryModel? _dbCategory;
        public CategoryModel? DbCategory
        {
            get
            {
                if (LazyLoader != null)
                {
                    return LazyLoader.Load(this, ref _dbCategory);
                }

                return _dbCategory;
            }
            set 
            {
                _dbCategory = value;
            }
        }

        //public CategoryModel? DbCategory { get; set; }

        public ICategoryModel? Category
        {
            get => DbCategory;
            set => DbCategory = value as CategoryModel;
        }

        public Guid? CategoryUid { get; set; }

        public virtual List<StockEntryModel>? DbStockEntries { get; set; }

        public List<IStockEntryModel>? StockEntries
        {
            get => DbStockEntries?.Cast<IStockEntryModel>()?.ToList();
            set => DbStockEntries = value?.Cast<StockEntryModel>()?.ToList();
        }

        public virtual List<StockOutputModel>? DbStockOutputs { get; set; }
        public List<IStockOutputModel>? StockOutputs
        {
            get => DbStockOutputs?.Cast<IStockOutputModel>()?.ToList();
            set => DbStockOutputs = value?.Cast<StockOutputModel>()?.ToList();
        }

        public virtual SupplierModel? DbSupplier { get; set; }
        public ISupplierModel? Supplier
        {
            get => DbSupplier;
            set => DbSupplier = value as SupplierModel;
        }

        public Guid? SupplierUid { get; set; }

        private ILazyLoader LazyLoader { get; set; }

        public ProductModel(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public ProductModel()
        {
            DbStockEntries = new List<StockEntryModel>();
            DbStockOutputs = new List<StockOutputModel>();
        }
    }
}
