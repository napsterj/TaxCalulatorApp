using Riok.Mapperly.Abstractions;
using TaxCalulator.API.Dtos;
using TaxCalulator.Entities.Entities;

namespace TaxCalulator.API.Mappers
{
    [Mapper]
    public partial class CountryMapper
    {
        public partial Country CountryDtoToCountry(CountryDto countryDto);
    }
}
