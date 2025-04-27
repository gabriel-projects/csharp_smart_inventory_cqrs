using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.SmartInventory.Interfaces.Entities
{
    public enum EOrderByType
    {
        Name,
        StockQuantity,
        UnitPrice,
        CreatedAt
    }

    public enum EOrderByDirection
    {
        Ascending,
        Descending
    }
}
