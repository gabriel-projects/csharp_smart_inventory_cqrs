using Api.GRRInnovations.SmartInventory.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Domain.Entities
{
    public class SupplierModel : BaseModel, ISupplierModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual List<ProductModel>? DbProducts { get; set; }

        public List<IProductModel>? Products
        {
            get => DbProducts?.Cast<IProductModel>()?.ToList();
            set => DbProducts = value?.Cast<ProductModel>()?.ToList();
        }

        public SupplierModel()
        {
            DbProducts = [];
        }
    }
}
