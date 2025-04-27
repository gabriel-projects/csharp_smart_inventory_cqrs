using Api.GRRInnovations.SmartInventory.Interfaces.Entities;

namespace Api.GRRInnovations.SmartInventory.Domain.Entities
{
    public class StockOutputModel : BaseModel, IStockOutputModel
    {
        public Guid ProductId { get; set; }
        public DateTime OutputDate { get; set; }
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
