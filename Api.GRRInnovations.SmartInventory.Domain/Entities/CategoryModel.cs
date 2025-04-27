using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Metadata;

namespace Api.GRRInnovations.SmartInventory.Domain.Entities
{
    public class CategoryModel : BaseModel, ICategoryModel
    {
        public string Name { get; set; }

        public List<ProductModel> _dbProducts;
        public List<ProductModel>? DbProducts
        {
            get => LazyLoader.Load(this, ref _dbProducts);
            set => _dbProducts = value;
        }

        public List<IProductModel>? Products
        {
            get => DbProducts?.Cast<IProductModel>()?.ToList();
            set => DbProducts = value?.Cast<ProductModel>()?.ToList();
        }

        private ILazyLoader LazyLoader { get; set; }

        public CategoryModel(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public CategoryModel()
        {
            DbProducts = new List<ProductModel>();
        }
    }
}
