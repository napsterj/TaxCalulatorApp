using TaxCalculator.Repo.Interface;
using TaxCalulator.Entities.Entities;
using TaxCalulator.Service.Interface;

namespace TaxCalulator.Service.Implementation
{
    public class TaxService(ITaxRepository taxRepository) : ITaxService
    {
        private readonly ITaxRepository _taxRepository = taxRepository;
        
        public (decimal, decimal) GetVatAndGrossValues(Price price)
        {
            return _taxRepository.GetVatAndGrossValues(price.NetPrice.Value, price.VatRate);
        }

        public (decimal, decimal) GetNetAndVatValues(Price price)
        {
            return _taxRepository.GetNetAndVatValues(price.GrossPrice.Value, price.VatRate);
        }

        public (decimal, decimal) GetNetAndGrossValues(Price price)
        {
            return _taxRepository.GetNetAndGrossValues(price.VatAmount.Value, price.VatRate);
        }

        public Task<IEnumerable<TaxRate>> GetTaxRatesByCountry(Country country) =>  _taxRepository.GetTaxRatesByCountry(country);
        
    }
}
