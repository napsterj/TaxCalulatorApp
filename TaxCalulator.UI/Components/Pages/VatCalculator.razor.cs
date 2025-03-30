using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Reflection;
using System.Threading.Tasks;
using TaxCalulator.UI.Common;
using TaxCalulator.UI.Dtos;
using TaxCalulator.UI.Dtos.Wrappers;
using TaxCalulator.UI.IServices.Interface;

namespace TaxCalulator.UI.Components.Pages
{
    public partial class VatCalculator(ITaxService taxService, 
                                       IMemoryCache cache)
    {
        private readonly ITaxService _taxService = taxService;
        private readonly IMemoryCache cache = cache;
        private List<TaxRateDto> taxRates = new();

        private bool isDisableNetPrice = false;
        private bool isDisableVatAmount = false;
        private bool isDisableGrossPrice = false;

        private string countryName { get; set; }
        private decimal vatRate { get; set; }
        private string selectedFlag = "";

        [Parameter]
        public PriceDto Price { get; set; } = new();

        public async Task FetchCountryVatRates(string CountryName)
        {
            var countryDto = new CountryDto
            {
                Name = CountryName,
            };

            countryName = CountryName;

            //Fetching the response from InMemory cache to save an api call and subequent db calls to fetch tax rates based
            //on country name, because tax rates do not change so often.
            List<TaxRateDto> taxRates = cache.Get<List<TaxRateDto>>(CountryName)!;
            
            if (taxRates == null || taxRates.Count == 0)
            {
                var response = await _taxService.GetTaxRatesByCountry(countryDto);
                var deserializedResponse = JsonConvert.DeserializeObject<DeserializeTaxRateHandler>(Convert.ToString(response.Result));
                taxRates = deserializedResponse.Result;
                
                //Saving the response in the cache for the duration of 2 days.
                taxRates = cache.Set<List<TaxRateDto>>(CountryName, taxRates, TimeSpan.FromDays(2));
            }
            

            if (taxRates.Count > 0)
                await SetSelectedVatRate(taxRates[0].Rate);

            Reset();

        }       

        public async Task GetPriceDetails(string flag)
        {
            ResponseDto responseDto = new();

            if (string.IsNullOrWhiteSpace(countryName))
            {
                return;
            }

            Price.CountryName = countryName;
            Price.VatRate = vatRate;
            
            selectedFlag = flag;

            if (Price.NetPrice > 0.00M || Price.VatAmount > 0.00M || Price.GrossPrice > 0.00M) 
            {
                if (flag == AppConstants.NET)
                {
                    responseDto = await _taxService.GetVatAndGrossValues(Price);
                    Price = DeserializeResponse(responseDto.Result);
                }

                else if (flag == AppConstants.VAT)
                {
                    responseDto = await _taxService.GetNetAndGrossValues(Price);
                    Price = DeserializeResponse(responseDto.Result);
                }
                else
                {
                    responseDto = await _taxService.GetNetAndVatValues(Price);
                    Price = DeserializeResponse(responseDto.Result);
                }
            }
            
        }

        protected override void OnInitialized()
        {
            isDisableVatAmount = true;
            isDisableGrossPrice = true;
        }

        public void ToggleEnableInputs(string flags)
        {
            if (flags == AppConstants.NET)
            {
                isDisableNetPrice = false;
                isDisableVatAmount = true;
                isDisableGrossPrice = true;

            }
            else if (flags == AppConstants.VAT)
            {
                isDisableNetPrice = true;
                isDisableVatAmount = false;
                isDisableGrossPrice = true;
            }
            else
            {
                isDisableNetPrice = true;
                isDisableVatAmount = true;
                isDisableGrossPrice = false;
            }

            Reset();
        }

        private async Task SetSelectedVatRate(decimal selectedVatRate)
        {
            vatRate = selectedVatRate;

            if (Price.NetPrice > 0.00M && Price.VatAmount > 0.00M && Price.GrossPrice > 0.00M)
                await GetPriceDetails(selectedFlag);

        }

        private PriceDto DeserializeResponse(object result)
        {
            var price = JsonConvert.DeserializeObject<DeserializeFinalOutput>
                                                        (Convert.ToString(result))!;
            return price.Result;
        }

        private void Reset()
        {
            Price.NetPrice = 0.00M;
            Price.VatAmount = 0.00M;
            Price.GrossPrice = 0.00M;
        } 
        
        

    }
}