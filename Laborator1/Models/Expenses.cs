
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Laborator1.Models
{
  
    public enum Type
    {
        food, 
        utilities,
        transportation,
        outing, 
        groceries,
        clothes, 
        electronics, 
        other
    };
    public class Expenses
    {
        public int Id { get; set; } 
        public string Description { get; set; }
        public double Sum { get; set; }
        public DateTime Date { get; set; }
        public Type Type { get; set; }
        public List<Comment> Comments{ get; set; }

    }
}
