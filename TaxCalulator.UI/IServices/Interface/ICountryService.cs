using TaxCalulator.UI.Dtos;

namespace TaxCalulator.UI.IServices.Interface
{
    public interface ICountryService
    {
        Task<ResponseDto> GetCountries();
    }
}
