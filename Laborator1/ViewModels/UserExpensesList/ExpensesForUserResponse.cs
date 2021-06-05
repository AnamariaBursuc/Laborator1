using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator1.ViewModels.UserExpensesList
{
    public class ExpensesForUserResponse
    {
            
        public ApplicationUserViewModel ApplicationUser { get; set; }
        public List<ExpenseViewModel> Expenses{ get; set; }
        public DateTime ExpensesListDateTime { get; set; }
    }
}

