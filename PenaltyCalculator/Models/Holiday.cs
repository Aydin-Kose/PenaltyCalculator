using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Models
{
    public class Holiday
    {
        [Key]
        public int Id { get; set; }
        public string HolidayName { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}
