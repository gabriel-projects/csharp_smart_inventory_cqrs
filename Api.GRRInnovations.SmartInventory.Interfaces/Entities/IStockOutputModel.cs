using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Interfaces.Entities
{
    public interface IStockOutputModel : IBaseModel
    {
        public Guid ProductId { get; set; }
        public DateTime OutputDate { get; set; }
        public int Quantity { get; set; }

        public IProductModel Product { get; set; }
    }
}
