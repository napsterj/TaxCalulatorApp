using Microsoft.EntityFrameworkCore;
using TaxCalculator.Repo.Interface;
using TaxCalulator.DAL;
using TaxCalulator.Entities.Entities;

namespace TaxCalculator.Repo.Implementation
{
    public class CountryRepository(CalculatorDbContext context) : ICountryRepository
    {
        private readonly CalculatorDbContext _context = context;

        public async Task<List<Country>> GetCountries()
        {
            return await _context.Countries.AsNoTracking().ToListAsync();
        }
    }
}
