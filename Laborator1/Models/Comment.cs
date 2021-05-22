namespace Laborator1.Models
{
    public class Comment
    {
   
        public long Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public int ExpenseId { get; set; }
        public Expenses Expense { get; set; }

    }
}