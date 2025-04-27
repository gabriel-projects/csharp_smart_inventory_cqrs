using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.SmartInventory.Domain.Wrappers.In
{
    public class WrapperInCategory<TCategory> : WrapperBase<TCategory, WrapperInCategory<TCategory>>
        where TCategory : ICategoryModel
    {
        [JsonPropertyName("name")]
        public string Name
        {
            get => Data.Name;
            set => Data.Name = value;
        }
    }
}
