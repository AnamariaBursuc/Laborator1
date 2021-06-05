

using AutoMapper;
using Laborator1.Models;
using Laborator1.ViewModels;
using Laborator1.ViewModels.UserExpensesList;

namespace Laborator1
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Expenses, ExpenseViewModel>().ReverseMap();//.ReverseMap();
                CreateMap<Comment, CommentsViewModel>().ReverseMap();
                CreateMap<Expenses, ExpenseWithCommentsViewModel>();
                CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
                CreateMap<UserExpensesList, ExpensesForUserResponse>().ReverseMap();
        }
        }
    }