using TaxCalulator.Entities.Entities;

namespace TaxCalculator.Repo.Interface
{
    public interface ITaxRepository
    {
        Task<(decimal, decimal)> CalculateValue(Price price, string countryName = "Austria");
    }
}
