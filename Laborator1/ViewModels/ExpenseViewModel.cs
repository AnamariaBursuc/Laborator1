using Laborator1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Type = Laborator1.Models.Type;

namespace Laborator1.ViewModels
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public double Sum { get; set; }
        public DateTime Date { get; set; }
        public Type Type { get; set; }
     
    }
}
