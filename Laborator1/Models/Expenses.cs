using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Laborator1.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public double Sum { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }

    }
}
