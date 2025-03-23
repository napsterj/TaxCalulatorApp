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
        Task<(decimal, decimal)> CalculateValues(Price price, decimal selectedTaxRate);
    }
}
