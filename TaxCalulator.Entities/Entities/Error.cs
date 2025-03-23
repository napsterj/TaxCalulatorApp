using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalulator.Entities.Entities
{
    public class Error
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }
}
