using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalulator.Entities.Entities;

namespace TaxCalulator.Service.Interface
{
    public interface ITaxService
    {        
        (decimal, decimal) GetVatAndGrossValues(Price price);
        (decimal, decimal) GetNetAndGrossValues(Price price);
        (decimal, decimal) GetNetAndVatValues(Price price);
    }
}
