
namespace Api.GRRInnovations.SmartInventory.Interfaces.Entities
{
    public interface ICategoryModel : IBaseModel
    {
        public string Name { get; set; }

        public List<IProductModel> Products { get; set; }
    }
}
