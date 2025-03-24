using TaxCalulator.Entities.Entities;

namespace TaxCalculator.Repo.Interface
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountries();
    }
}
