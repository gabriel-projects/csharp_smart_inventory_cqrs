using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Interfaces.Entities
{
    public interface IProductModel : IBaseModel
    {
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ICategoryModel Category { get; set; }
        public ISupplierModel Supplier { get; set; }
        public List<IStockEntryModel> StockEntries { get; set; }
        public List<IStockOutputModel> StockOutputs { get; set; }
    }
}
