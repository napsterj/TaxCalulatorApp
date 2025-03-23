using System.ComponentModel.DataAnnotations;

namespace TaxCalulator.API.Dtos
{
    public class CountryDto
    {
        [Required(ErrorMessage = "Name of country is mandatory to calculate the price with VAT.")]
        public string Name { get; set; } = string.Empty;
    }
}
