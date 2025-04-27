using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Domain.Wrappers.Out
{
    public class WrapperOutProduct : WrapperBase<IProductModel>
    {
        public WrapperOutProduct() : base(null) { }

        public WrapperOutProduct(IProductModel data) : base(data)
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

        [JsonPropertyName("stock_quantity")]
        public int StockQuantity
        {
            get => Data.StockQuantity;
            set => Data.StockQuantity = value;
        }

        [JsonPropertyName("unit_price")]
        public decimal UnitPrice
        {
            get => Data.UnitPrice;
            set => Data.UnitPrice = value;
        }


        //TODO: INCLUDE RETURN CETEGORY AND SUPPLIER

        public new static async Task<WrapperOutProduct> From(IProductModel model)
        {
            var wrapper = new WrapperOutProduct();
            await wrapper.Populate(model).ConfigureAwait(false);

            return wrapper;
        }

        public new static async Task<List<WrapperOutProduct>> From(List<IProductModel> models)
        {
            var result = new List<WrapperOutProduct>();

            foreach (var model in models)
            {
                var wrapper = await From(model).ConfigureAwait(false);
                result.Add(wrapper);
            }

            return result;
        }
    }
}
