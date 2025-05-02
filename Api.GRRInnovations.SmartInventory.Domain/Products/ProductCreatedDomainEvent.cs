using Api.GRRInnovations.SmartInventory.Domain.Entities;

namespace Api.GRRInnovations.SmartInventory.Domain.Products
{
    public sealed record ProductCreatedDomainEvent : IDomainEvent
    {
        public Guid ProductUid { get; }

        public ProductCreatedDomainEvent(Guid productUid)
        {
            ProductUid = productUid;
        }
    }
}
