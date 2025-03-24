using TaxCalulator.UI.Dtos;

namespace TaxCalulator.UI.IServices.Interface
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
