using Microsoft.EntityFrameworkCore;
using TaxCalculator.Repo.Interface;
using TaxCalulator.DAL;
using TaxCalulator.Entities.Entities;

namespace TaxCalculator.Repo.Implementation
{
    public class TaxRepository(CalculatorDbContext context) : ITaxRepository
    {
        private readonly CalculatorDbContext _context = context;

        public async Task<(decimal, decimal)> CalculateValue(Price price, 
                                                  string countryName = "Austria")
        {            
            //1. Get tax rates of the country
            var country = await _context.Countries.FirstOrDefaultAsync(ctr => 
                                                        ctr.Name.ToUpper() == countryName.ToUpper());
            if (country == null) 
            {
                throw new Exception($"VAT details are not available currently for {countryName}");
            }                        

            //2. Based on VAT rate calculate all the values
            if(price.NetPrice != 0.00M)
            {
                var (vatValue, grossAmount) = GetVatAndGrossValues(price.NetPrice.Value, price.VatRate);
                return (vatValue, grossAmount);
            }
            else if(price.VatAmount != 0.00M)
            {
                var (netValue, grossAmount) = GetNetAndGrossValues(price.VatAmount.Value,
                                                 price.VatRate);
                return (netValue, grossAmount);
            }

            var (netAmount, vatAmount) = GetNetAndVatValues(price.NetPrice.Value,
                                      price.VatRate);

            return (netAmount, vatAmount);
        }

        private (decimal,decimal) GetVatAndGrossValues(decimal netAmount, decimal selectedTaxRate)
        {
            var vatAmount = (netAmount * selectedTaxRate) / 100;
            var grossAmount = netAmount + vatAmount;
            return (vatAmount, grossAmount);
        }

        private (decimal, decimal) GetNetAndGrossValues(decimal vatAmount, decimal selectedTaxRate)
        {
            var netAmount = (vatAmount * 100) / selectedTaxRate;
            var grossAmount = netAmount + vatAmount;
            return (netAmount, grossAmount);
        }

        private (decimal, decimal) GetNetAndVatValues(decimal grossAmount, decimal selectedTaxRate)
        {
            var vatAmount = selectedTaxRate / 100;
            var netAmount = grossAmount - vatAmount;
            return (netAmount, vatAmount);
        }
    }
}
