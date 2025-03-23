using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalulator.Entities.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public ICollection<TaxRate> TaxRates { get; set; }

    }
}
