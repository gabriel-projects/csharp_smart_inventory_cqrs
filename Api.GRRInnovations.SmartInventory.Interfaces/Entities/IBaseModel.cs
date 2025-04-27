namespace Api.GRRInnovations.SmartInventory.Interfaces.Entities
{
    public interface IBaseModel
    {
        Guid Uid { get; set; }

        Guid UpdatedBy { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }

        bool IsDeleted {  get; set; }
    }
}
