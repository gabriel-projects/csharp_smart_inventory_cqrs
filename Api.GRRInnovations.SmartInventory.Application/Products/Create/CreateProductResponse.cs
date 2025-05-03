using Api.GRRInnovations.SmartInventory.Domain.Wrappers;
using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Application.Products.Create
{
    public class CreateProductResponse : WrapperBase<IProductModel>
    {
        public CreateProductResponse() : base(null) { }

        public CreateProductResponse(IProductModel data) : base(data)
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

        public new static async Task<CreateProductResponse> From(IProductModel model)
        {
            var wrapper = new CreateProductResponse();
            await wrapper.Populate(model).ConfigureAwait(false);

            return wrapper;
        }

        public new static async Task<List<CreateProductResponse>> From(List<IProductModel> models)
        {
            var result = new List<CreateProductResponse>();

            foreach (var model in models)
            {
                var wrapper = await From(model).ConfigureAwait(false);
                result.Add(wrapper);
            }

            return result;
        }
    }
}
