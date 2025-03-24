using TaxCalulator.UI.IServices.Interface;

namespace TaxCalulator.UI.Components.Pages
{
    public partial class VatCalculator(ITaxService taxService)
    {
        private readonly ITaxService taxService = taxService;
    }
}