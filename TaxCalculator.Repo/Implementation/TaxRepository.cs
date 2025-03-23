using Microsoft.EntityFrameworkCore;
using TaxCalculator.Repo.Interface;
using TaxCalulator.DAL;
using TaxCalulator.Entities.Entities;

namespace TaxCalculator.Repo.Implementation
{
    public class TaxRepository(CalculatorDbContext context) : ITaxRepository
    {
        private readonly CalculatorDbContext _context = context;
        
        public (decimal,decimal) GetVatAndGrossValues(decimal netAmount, decimal selectedTaxRate)
        {
            var vatAmount = (netAmount * selectedTaxRate) / 100;
            var grossAmount = netAmount + vatAmount;
            return (vatAmount, grossAmount);
        }

        public (decimal, decimal) GetNetAndGrossValues(decimal vatAmount, decimal selectedTaxRate)
        {
            var netAmount = (vatAmount * 100) / selectedTaxRate;
            var grossAmount = netAmount + vatAmount;
            return (netAmount, grossAmount);
        }

        public (decimal, decimal) GetNetAndVatValues(decimal grossAmount, decimal selectedTaxRate)
        {
            var netAmount = grossAmount * 100 / (selectedTaxRate + 100);
            var vatAmount = grossAmount - netAmount;
            return (netAmount, vatAmount);
        }
    }
}
