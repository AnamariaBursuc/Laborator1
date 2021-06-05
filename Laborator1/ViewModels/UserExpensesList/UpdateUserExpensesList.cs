using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator1.ViewModels.UserExpensesList
{
    public class UpdateUserExpensesList
    {
        public int Id { get; set; }
        public List<int> ExpensesIds { get; set; }
    }
}

