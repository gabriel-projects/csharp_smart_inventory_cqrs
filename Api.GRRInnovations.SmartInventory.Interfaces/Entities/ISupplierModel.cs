using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Interfaces.Entities
{
    public interface ISupplierModel : IBaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<IProductModel> Products { get; set; }
    }
}
