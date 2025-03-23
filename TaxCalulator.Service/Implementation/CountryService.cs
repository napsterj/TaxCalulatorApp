using TaxCalculator.Repo.Interface;
using TaxCalulator.Entities.Entities;
using TaxCalulator.Service.Interface;

namespace TaxCalulator.Service.Implementation
{
    public class CountryService(ICountryRepository countryRepository) : ICountryService
    {
        private readonly ICountryRepository _countryRepository = countryRepository;

        public Task<IEnumerable<Country>> GetCountries()
        {
            return _countryRepository.GetCountries();
        }
    }
}
