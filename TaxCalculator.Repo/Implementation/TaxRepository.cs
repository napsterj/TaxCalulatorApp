using Microsoft.EntityFrameworkCore;
using TaxCalculator.Repo.Interface;
using TaxCalulator.DAL;
using TaxCalulator.Entities.Entities;

namespace TaxCalculator.Repo.Implementation
{
    public class TaxRepository(CalculatorDbContext context) : ITaxRepository
    {
        private readonly CalculatorDbContext _context = context;

        public (decimal, decimal) GetVatAndGrossValues(decimal netAmount, decimal selectedTaxRate)
        {
            var vatAmount = (netAmount * selectedTaxRate) / 100;
            var grossAmount = netAmount + vatAmount;
            return (Math.Round(vatAmount,2), 
                    Math.Round(grossAmount,2));
        }

        public (decimal, decimal) GetNetAndGrossValues(decimal vatAmount, decimal selectedTaxRate)
        {
            var netAmount = (vatAmount * 100) / selectedTaxRate;
            var grossAmount = netAmount + vatAmount;
            return (Math.Round(netAmount,2), 
                    Math.Round(grossAmount, 2));
        }

        public (decimal, decimal) GetNetAndVatValues(decimal grossAmount, decimal selectedTaxRate)
        {
            var netAmount = grossAmount * 100 / (selectedTaxRate + 100);
            var vatAmount = grossAmount - netAmount;
            return (Math.Round(netAmount, 2), 
                    Math.Round(vatAmount, 2));
        }

        public async Task<IEnumerable<TaxRate>> GetTaxRatesByCountry(Country country)
        {
            return await _context.TaxRates
                                 .AsNoTracking()
                                 .Where(c => c.Country.Id == country.Id || 
                                        c.Country.Name.ToUpper() == country.Name.ToUpper())
                                 .ToListAsync();
        }
    }
}
