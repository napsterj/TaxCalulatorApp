namespace TaxCalulator.UI.Dtos
{
    public class PriceDto
    {
        public string CountryName { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrossPrice { get; set; }
        public decimal VatRate { get; set; }
    }
}
