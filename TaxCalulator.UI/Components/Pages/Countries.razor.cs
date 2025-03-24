using Newtonsoft.Json;
using TaxCalulator.UI.Dtos;
using TaxCalulator.UI.Dtos.Wrappers;
using TaxCalulator.UI.IServices.Interface;

namespace TaxCalulator.UI.Components.Pages
{
    public partial class Countries(ICountryService countryService)
    {
        private readonly ICountryService _countryService = countryService;

        
        public HashSet<CountryDto> CountryList { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var response = await _countryService.GetCountries();
            var countriesHandler = JsonConvert.DeserializeObject<DeserializeDtoHandler>(Convert.ToString(response.Result));
            CountryList = countriesHandler.Result;
        }
        
    }
}