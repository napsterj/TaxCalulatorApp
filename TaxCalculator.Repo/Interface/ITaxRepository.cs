using TaxCalulator.Entities.Entities;

namespace TaxCalculator.Repo.Interface
{
    public interface ITaxRepository
    {        
        (decimal, decimal) GetVatAndGrossValues(decimal netAmount, decimal selectedTaxRate);
        (decimal, decimal) GetNetAndGrossValues(decimal vatAmount, decimal selectedTaxRate);
        (decimal, decimal) GetNetAndVatValues(decimal grossAmount, decimal selectedTaxRate);
    }
}
