namespace TaxCalulator.API.Dtos
{
    public class PriceDto
    {
        public CountryDto CountryDto { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrossPrice { get; set; }
        public decimal VatRate { get; set; }
    }
}
