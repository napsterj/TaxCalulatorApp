using TaxCalulator.UI.Dtos;
using TaxCalulator.UI.IServices.Interface;

namespace TaxCalulator.UI.IServices
{
    public class TaxService(IBaseService baseService) : ITaxService
    {
        private readonly IBaseService _baseService = baseService;

        public Task<ResponseDto> GetNetAndGrossValues(PriceDto priceDto)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = Common.AppConstants.ApiType.POST,
                
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
