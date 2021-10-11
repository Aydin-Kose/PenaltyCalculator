using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PenaltyCalculator.Data;
using PenaltyCalculator.Models;
using PenaltyCalculator.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new PenaltyCalculatorViewModel();
            model.CountriesDropDown = await FillCountriesDropdownAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(PenaltyCalculatorViewModel model)
        {
            model = new PenaltyCalculatorViewModel();
            model.CountriesDropDown = await FillCountriesDropdownAsync();
            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> FillCountriesDropdownAsync()
        {
            var Countries = await context.Countries.ToListAsync();
            return Countries.Select(n => new SelectListItem
            {
                Text = n.CountryName,
                Value = n.Id.ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(PenaltyCalculatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Book.CalculatedBussinessDays = await CalculateBussinesDaysAsync((DateTime)model.Book.CheckOutDate, (DateTime)model.Book.ReturnDate, model.Country.Id);
                model.Book.CalculatedPenalty = await CalculatePenaltyAmountAsync((int)model.Book.CalculatedBussinessDays, model.Country.Id);
            }
            model.CountriesDropDown = await FillCountriesDropdownAsync();
            return View("Index", model);
        }

        private async Task<int> CalculateBussinesDaysAsync(DateTime startDate, DateTime endDate, int SelectedCountryId)
        {
            var totalDays = 0;
            var OffDays = await context.OffDays.Where(n => n.Country.Id == SelectedCountryId).ToListAsync();
            var Holidays = await context.Holidays.Where(n => n.Country.Id == SelectedCountryId).ToListAsync();
            while (startDate <= endDate)
            {
                if (OffDays.All(n => n.Day != (int)startDate.DayOfWeek))//Offday değilse bussiness day  1 artırılır.
                {
                    if (!Holidays.Any(n => (n.Date.Month == startDate.Month && n.Date.Day == startDate.Day)))
                    {
                        ++totalDays;
                    }
                }
                startDate = startDate.AddDays(1);
            }

            return totalDays;
        }

        private async Task<string> CalculatePenaltyAmountAsync(int calculatedBussinessDays, int SelectedCountryId)
        {
            var country = await context.Countries.FirstOrDefaultAsync(n => n.Id == SelectedCountryId);
            if (calculatedBussinessDays>10)
            {
                return (calculatedBussinessDays - 10) * country.DailyAmount + " " + country.CurrencyType;
            }
            else
            {
                return "No Penalty";
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
