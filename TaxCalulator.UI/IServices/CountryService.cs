using TaxCalulator.UI.Common;
using TaxCalulator.UI.Dtos;
using TaxCalulator.UI.IServices.Interface;

namespace TaxCalulator.UI.IServices
{
    public class CountryService(IBaseService baseService, IConfiguration configuration) : ICountryService
    {
        private readonly IBaseService _baseService = baseService;
        private readonly IConfiguration _configuration = configuration;
        private string baseUrl = string.Empty;

        public Task<ResponseDto> GetCountries()
        {
            baseUrl = _configuration["ApiConfig:BaseUrl"]!;

            return _baseService.SendAsync(new RequestDto
            {
                ApiType = AppConstants.ApiType.GET,
                Url = $"{baseUrl}/api/Tax/get/countries",                
            });
        }
    }
}
