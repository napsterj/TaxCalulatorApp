using TaxCalulator.UI.Dtos;

namespace TaxCalulator.UI.IServices.Interface
{
    public interface ITaxService
    {
        ResponseDto GetVatAndGrossValues(PriceDto priceDto);
        ResponseDto GetNetAndGrossValues(PriceDto priceDto);
        ResponseDto GetNetAndVatValues(PriceDto priceDto);
    }
}
