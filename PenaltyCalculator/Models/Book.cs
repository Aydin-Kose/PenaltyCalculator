using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Models
{
    public class Book
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Check Out Date")]
        public DateTime? CheckOutDate { get; set; } = null;
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Return Date")]
        public DateTime? ReturnDate { get; set; } = null;
        [DisplayName("Calculated Bussiness Days")]
        public int? CalculatedBussinessDays { get; set; }
        [DisplayName("Calculated Penalty")]
        public string CalculatedPenalty { get; set; }
    }
}
