using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Domain.Wrappers.Out
{
    public class WrapperOutCategory : WrapperBase<ICategoryModel>
    {
        public WrapperOutCategory() : base(null) { }

        public WrapperOutCategory(ICategoryModel data) : base(data)
        {

        }

        [JsonPropertyName("uid")]
        public Guid Uid
        {
            get => Data.Uid;
            set => Data.Uid = value;
        }


        [JsonPropertyName("name")]
        public string Name
        {
            get => Data.Name;
            set => Data.Name = value;
        }

        public new static async Task<WrapperOutCategory> From(ICategoryModel model)
        {
            var wrapper = new WrapperOutCategory();
            await wrapper.Populate(model).ConfigureAwait(false);

            return wrapper;
        }

        public new static async Task<List<WrapperOutCategory>> From(List<ICategoryModel> models)
        {
            var result = new List<WrapperOutCategory>();

            foreach (var model in models)
            {
                var wrapper = await From(model).ConfigureAwait(false);
                result.Add(wrapper);
            }

            return result;
        }
    }
}
