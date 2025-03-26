using Microsoft.AspNetCore.Components;
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

        [Parameter]
        public EventCallback<string> CountryName { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var response = await _countryService.GetCountries();
                var countriesHandler = JsonConvert.DeserializeObject<DeserializeDtoHandler>(Convert.ToString(response.Result));
                CountryList = countriesHandler.Result;
                StateHasChanged();
            }
        }
                              
        public void SelectCountry(ChangeEventArgs args)
        {
            CountryName.InvokeAsync(Convert.ToString(args.Value));
        }
    }
}