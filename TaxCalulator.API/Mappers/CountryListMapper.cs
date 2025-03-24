using Riok.Mapperly.Abstractions;
using TaxCalulator.API.Dtos;
using TaxCalulator.Entities.Entities;

namespace TaxCalulator.API.Mappers
{
    [Mapper]
    public partial class CountryListMapper
    {
        public partial List<CountryDto> CountryToCountryDto(List<Country> countries);
    }
}
