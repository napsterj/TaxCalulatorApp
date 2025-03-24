using Riok.Mapperly.Abstractions;
using TaxCalulator.API.Dtos;
using TaxCalulator.Entities.Entities;

namespace TaxCalulator.API.Mappers
{
    [Mapper]
    public partial class TaxRateMapper
    {
        public partial List<TaxRateDto> TaxRateToTaxRateDto(List<TaxRate> taxRate);
    }
}
