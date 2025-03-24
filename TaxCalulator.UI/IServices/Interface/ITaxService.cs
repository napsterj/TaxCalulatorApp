using TaxCalulator.UI.Dtos;

namespace TaxCalulator.UI.IServices.Interface
{
    public interface ITaxService
    {
        Task<ResponseDto> GetVatAndGrossValues(PriceDto priceDto);
        Task<ResponseDto> GetNetAndGrossValues(PriceDto priceDto);
        Task<ResponseDto> GetNetAndVatValues(PriceDto priceDto);
    }
}
