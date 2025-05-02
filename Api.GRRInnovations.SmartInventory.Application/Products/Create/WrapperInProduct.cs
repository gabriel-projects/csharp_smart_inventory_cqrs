using Api.GRRInnovations.SmartInventory.Domain.Wrappers;
using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Application.Products.Create
{
    public class WrapperInProduct<TProduct> : WrapperBase<TProduct, WrapperInProduct<TProduct>>, ICommand<Guid>
        where TProduct : IProductModel
    {
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

        [JsonPropertyName("category_uid")]
        public Guid CategoryUid { get; set; }

        [JsonPropertyName("supplier_uid")]
        public Guid SupplierUid { get; set; }
    }
}
