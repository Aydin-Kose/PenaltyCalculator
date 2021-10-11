using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Country Name")]
        public string CountryName { get; set; }
        public string CurrencyType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DailyAmount { get; set; }
    }
}
