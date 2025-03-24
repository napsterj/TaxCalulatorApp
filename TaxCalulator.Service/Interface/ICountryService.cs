using TaxCalulator.Entities.Entities;

namespace TaxCalulator.Service.Interface
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountries();
    }
}
