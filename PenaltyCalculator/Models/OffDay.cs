using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Models
{
    public class OffDay
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Day { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}
