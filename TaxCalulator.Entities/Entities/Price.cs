using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalulator.Entities.Entities
{
    [NotMapped]
    public class Price
    {        
        public decimal? NetPrice { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrossPrice { get; set; }
    }
}
