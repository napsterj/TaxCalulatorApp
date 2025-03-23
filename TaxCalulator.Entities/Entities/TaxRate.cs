using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalulator.Entities.Entities
{
    public class TaxRate
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(14,2)")]
        public decimal Rate { get; set; } 

        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
