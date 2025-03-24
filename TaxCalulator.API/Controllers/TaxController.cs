using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaxCalulator.API.Common;
using TaxCalulator.API.Dtos;
using TaxCalulator.API.Mappers;
using TaxCalulator.Entities.Entities;
using TaxCalulator.Service.Interface;

namespace TaxCalulator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController(ITaxService taxService, ICountryService countryService) : ControllerBase
    {
        private readonly ITaxService _taxService = taxService;
        private readonly ICountryService _countryService = countryService;

        [HttpGet("get/countries")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryService.GetCountries();
                       
            if (countries == null || !countries.Any())
            {
                return NotFound(BuildResponseDto(HttpStatusCode.NotFound, AppConstants.NO_COUNTRIES));
            }

            var countryListMapper = new CountryListMapper();
            var countriesDto = countryListMapper.CountryToCountryDto(countries);
            
            return Ok(BuildResponseDto(HttpStatusCode.OK, "", countriesDto));
        }

        [HttpPost("get/taxrates/bycountry")]
        public async Task<IActionResult> GetTaxRatesByCountry([FromBody] CountryDto countryDto)
        {
            if (countryDto == null || string.IsNullOrWhiteSpace(countryDto.Name))
            {
                var responseDto = BuildResponseDto(HttpStatusCode.BadRequest, AppConstants.COUNTRY_REQUIRED);
                return BadRequest(responseDto);
            }

            var countryMapper = new CountryMapper();
            var taxMapper = new TaxRateMapper();

            var taxRates = await _taxService.GetTaxRatesByCountry(countryMapper.CountryDtoToCountry(countryDto));

            if (taxRates == null || !taxRates.Any())
            {
                var responseDto = BuildResponseDto(HttpStatusCode.NotFound,
                                                   AppConstants.NO_VAT_RATES_FOUND);
                return NotFound(responseDto);
            }

            var taxRateDto = taxMapper.TaxRateToTaxRateDto([.. taxRates]);

            return Ok(BuildResponseDto(HttpStatusCode.OK, "", taxRateDto));
        }

        [HttpPost("calculate/price/details")]
        public async Task<IActionResult> GetPriceDetails([FromBody]PriceDto priceDto)
        {
            var countries = await _countryService.GetCountries();

            if (countries == null)
            {
                return NotFound(BuildResponseDto(HttpStatusCode.NotFound,
                                                 AppConstants.NO_COUNTRIES));
            }

            var isCountryNotListed = countries.Exists(c => c.Name.ToUpper() != priceDto.CountryName.ToUpper());
            
            if (string.IsNullOrWhiteSpace(priceDto.CountryName) || 
                isCountryNotListed)
            {
                throw new BadHttpRequestException(AppConstants.INVALID_COUNTRY);
            }

            if (priceDto.NetPrice <= 0.00M &&
                priceDto.VatAmount <= 0.00M &&
                priceDto.GrossPrice <= 0.00M)
            {
                throw new BadHttpRequestException("Net price, Vat amount and Gross price " +
                                        "cannot be 0 or less at the same time.");
            }

            var mapper = new PriceMapper();
            var price = mapper.PriceDtoToPrice(priceDto);

            return Ok(GetResultWithReponse(price));
        }
        
        private ResponseDto GetResultWithReponse(Price price)
        {
            if (price.NetPrice != 0.00M)
            {
                var (vatValue, grossAmount) = _taxService.GetVatAndGrossValues(price);
                return new ResponseDto
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Result = new { vatAmount = vatValue, grossValue = grossAmount }
                };

            }
            else if (price.VatAmount != 0.00M)
            {
                var (netValue, grossAmount) = _taxService.GetNetAndGrossValues(price);
                return new ResponseDto
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Result = new { netAmount = netValue, grossValue = grossAmount }
                };
            }

            var (netAmount, vatAmount) = _taxService.GetNetAndVatValues(price);

            return BuildResponseDto(System.Net.HttpStatusCode.OK, "", new { netAmount = netAmount, vatValue = vatAmount });

        }

        private ResponseDto BuildResponseDto(HttpStatusCode statusCode, string? error, object? data = null)
        {
            return new ResponseDto
            {
                StatusCode = statusCode,
                Error = error,
                Result = data
            };
        }
    }
}
