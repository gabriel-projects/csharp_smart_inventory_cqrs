namespace Api.GRRInnovations.SmartInventory.Interfaces.Entities
{
    public interface IStockEntryModel : IBaseModel
    {
        public Guid ProductId { get; set; }
        public DateTime EntryDate { get; set; }
        public int Quantity { get; set; }
        public IProductModel Product { get; set; }
    }
}
