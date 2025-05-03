using Api.GRRInnovations.SmartInventory.Domain.Wrappers;
using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Application.Products.Get
{
    public class ProductResponse : WrapperBase<IProductModel>
    {
        public ProductResponse() : base(null) { }

        public ProductResponse(IProductModel data) : base(data)
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

        public new static async Task<ProductResponse> From(IProductModel model)
        {
            var wrapper = new ProductResponse();
            await wrapper.Populate(model).ConfigureAwait(false);

            return wrapper;
        }

        public new static async Task<List<ProductResponse>> From(List<IProductModel> models)
        {
            var result = new List<ProductResponse>();

            foreach (var model in models)
            {
                var wrapper = await From(model).ConfigureAwait(false);
                result.Add(wrapper);
            }

            return result;
        }
    }
}
