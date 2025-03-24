using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaxCalulator.API.Dtos
{
    public class TaxRateDto
    {        
        public int Id { get; set; }
        
        public decimal Rate { get; set; }
    }
}
