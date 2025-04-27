using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Domain.Entities
{
    public class StockEntryModel : BaseModel, IStockEntryModel
    {
        public Guid ProductId { get; set; }
        public DateTime EntryDate { get; set; }
        public int Quantity { get; set; }
        public virtual ProductModel? DbProduct { get; set; }
        public IProductModel? Product
        {
            get => DbProduct;
            set => DbProduct = value as ProductModel;
        }

        public Guid? ProductUid { get; set; }
    }
}
