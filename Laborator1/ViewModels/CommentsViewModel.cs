using Laborator1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator1.ViewModels
{
    public class CommentsViewModel
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        
    }
}
