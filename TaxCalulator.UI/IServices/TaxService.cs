using TaxCalulator.UI.Dtos;
using TaxCalulator.UI.IServices.Interface;

namespace TaxCalulator.UI.IServices
{
    public class TaxService : ITaxService
    {
        private readonly IBaseService _baseService;
        private readonly IConfiguration _configuration;
     
        private string _baseUrl = string.Empty;
        public TaxService(IBaseService baseService, IConfiguration configuration)
        {
            _baseService = baseService;
            _configuration = configuration;
            _baseUrl = _configuration["ApiConfig:BaseUrl"]!;
        }

        public Task<ResponseDto> GetNetAndGrossValues(PriceDto priceDto)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = Common.AppConstants.ApiType.POST,
                Url = $"{_baseUrl}/api/Tax/calculate/price/details",
                Data = priceDto
            });
        }

        public Task<ResponseDto> GetNetAndVatValues(PriceDto priceDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetVatAndGrossValues(PriceDto priceDto)
        {
            throw new NotImplementedException();
        }
    }
}
