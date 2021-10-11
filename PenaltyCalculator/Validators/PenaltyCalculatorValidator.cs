using FluentValidation;
using PenaltyCalculator.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Validators
{
    public class PenaltyCalculatorValidator : AbstractValidator<PenaltyCalculatorViewModel>
    {
        public PenaltyCalculatorValidator()
        {
            RuleFor(x => x.Book.CheckOutDate).NotEmpty().WithMessage("Check Out Date Cannot be empty")
                .LessThan(x=>x.Book.ReturnDate).WithMessage("CheckOutDate needs to be less than ReturnDate");
            RuleFor(x => x.Book.ReturnDate).NotEmpty().WithMessage("Return Date Cannot be empty")
                .GreaterThan(x=>x.Book.CheckOutDate).WithMessage("Return Date needs to be greater than Check Out Date");
            RuleFor(x => x.Book.ReturnDate).NotEmpty().WithMessage("Return Date Cannot be empty");
            RuleFor(x => x.Country.Id).NotEqual(0).WithMessage("Country selection is required.");
        }
    }
}
