using Riok.Mapperly.Abstractions;
using TaxCalulator.API.Dtos;
using TaxCalulator.Entities.Entities;

namespace TaxCalulator.API.Mappers
{
    [Mapper]
    public partial class PriceMapper
    {
        public partial Price PriceDtoToPrice(PriceDto priceDto);
    }
}
