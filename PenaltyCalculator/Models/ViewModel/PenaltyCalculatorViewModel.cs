using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Models.ViewModel
{
    public class PenaltyCalculatorViewModel
    {
        public Book Book { get; set; } = new Book();
        public IEnumerable<SelectListItem> CountriesDropDown { get; set; }
        public Country Country { get; set; }
    }
}
