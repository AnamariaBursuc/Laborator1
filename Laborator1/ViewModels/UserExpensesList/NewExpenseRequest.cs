using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator1.ViewModels.UserExpensesList
{
    public class NewExpenseRequest
    {
        public List<int> ExpensesListedIds { get; set; }
        public DateTime? ExpenseListDateTime { get; set; }
    }
}

