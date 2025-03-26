using System.ComponentModel.DataAnnotations;

namespace TaxCalulator.UI.Dtos
{
    public class PriceDto
    {
        [Required(ErrorMessage ="Please select the country for which Price/VAT is to be calculated.")]
        public string CountryName { get; set; }
        
        [Required(ErrorMessage = "Please select the VatRate (%).")]
        public decimal VatRate { get; set; }
        
        private decimal? _netPrice { get; set; }
        public decimal? NetPrice { get; set; }
            
        public decimal? VatAmount { get; set; }
        public decimal? GrossPrice { get; set; }               
    }
}
