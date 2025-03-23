using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("calculate/price/details")]
        public async Task<IActionResult> GetPriceDetails([FromBody]PriceDto priceDto)
        {
            var countries = await _countryService.GetCountries();

            if (countries == null)
            {
                return NotFound("Please add countries in the application to " +
                                "perform price/vat calculation.");
            }

            if (string.IsNullOrWhiteSpace(priceDto.CountryName))
            {
                throw new BadHttpRequestException("Please supply the country name for a " +
                                                  "valid price/VAT calculation.");
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

            return new ResponseDto
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Result = new { netAmount = netAmount, vatValue = vatAmount }
            };
           
        }
    }
}
