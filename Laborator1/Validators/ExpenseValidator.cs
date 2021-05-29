using FluentValidation;
using Laborator1.Data;
using Laborator1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Laborator1.Validators
{
    public class ExpenseValidator : AbstractValidator<ExpenseViewModel>
    {

        private readonly ApplicationDbContext _context;


        public ExpenseValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(x => x.Sum).GreaterThan(0).WithMessage("Please ensure that you have entered your expense sum greater than 0");
            RuleFor(x => x.Description).Length(3, 500).WithMessage("Please ensure that you have entered the description of the expense"); ;
            RuleFor(x => x.Date).LessThan(x => DateTime.Now).WithMessage("That date hasn't come yet"); ;
            RuleFor(x => x.Type).IsInEnum();
        }

        public ApplicationDbContext Context => _context;
    }
}
