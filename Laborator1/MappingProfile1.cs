

using AutoMapper;
using Laborator1.Models;
using Laborator1.ViewModels;

namespace Laborator1
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Expenses, ExpenseViewModel>();//.ReverseMap();
                CreateMap<Comment, CommentsViewModel>();
                CreateMap<Expenses, ExpenseWithCommentsViewModel>();
            }
        }
    }