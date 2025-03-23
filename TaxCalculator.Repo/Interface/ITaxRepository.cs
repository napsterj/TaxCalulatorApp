using TaxCalulator.Entities.Entities;

namespace TaxCalculator.Repo.Interface
{
    public interface ITaxRepository
    {
        Task<decimal> CalculateValue(Price price);
    }
}
