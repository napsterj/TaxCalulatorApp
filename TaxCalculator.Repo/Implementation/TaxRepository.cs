using TaxCalculator.Repo.Interface;
using TaxCalulator.Entities.Entities;

namespace TaxCalculator.Repo.Implementation
{
    public class TaxRepository : ITaxRepository
    {        
        public Task<decimal> CalculateValue(Price price)
        {
            throw new NotImplementedException();
        }
    }
}
