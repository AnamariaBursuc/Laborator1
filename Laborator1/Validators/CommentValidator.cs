using FluentValidation;
using Laborator1.Data;
using Laborator1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator1.Validators
{
    public class CommentValidator : AbstractValidator<CommentsViewModel>
    {

        private readonly ApplicationDbContext _context;
        public CommentValidator(ApplicationDbContext context)
        {
            _context = context;
          

                RuleFor(c => c.Text).Length(1, 500).WithMessage("Please ensure that you have entered the comment");
              //  RuleFor(c => c.Important).NotEmpty().WithMessage("You must choose true or false");
            
        }
    }
}

