using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Repo.Interface;
using TaxCalulator.Entities.Entities;
using TaxCalulator.Service.Interface;

namespace TaxCalulator.Service.Implementation
{
    public class TaxService(ITaxRepository taxRepository) : ITaxService
    {
        private readonly ITaxRepository _taxRepository = taxRepository;

        public Task<(decimal, decimal)> CalculateValues(Price price, decimal selectedTaxRate)
        {
            throw new NotImplementedException();
        }
    }
}
