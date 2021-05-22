using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laborator1.Data;
using Laborator1.Models;
using Laborator1.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Laborator1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExpensesController> _logger;
        private readonly IMapper _mapper;

        public ExpensesController(ApplicationDbContext context, ILogger<ExpensesController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            
        }


        [HttpGet]
        [Route("filter/{minSum}")]
        public ActionResult<IEnumerable<Expenses>> FilterExpenses(int minSum)
        {
            var query = _context.Expenses.Where(e => e.Sum >= minSum);
            _logger.LogInformation(query.ToQueryString());
            return query.ToList();
        }
        
        
        // GET: api/Expenses   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expenses>>> GetExpenses(int? minSum)
        {
            if (minSum == null)
            {
                return await _context.Expenses.ToListAsync();
            }
            return await _context.Expenses.Where(c => c.Sum >= minSum).ToListAsync();
        }

        // GET: api/Expenses/Filter
        [HttpGet]
        [Route("filter")]
        public ActionResult<IEnumerable<Expenses>> GetExpensesByDate(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null || toDate == null)
            {
                return _context.Expenses.ToList();
            }
            var ExpensesList = _context.Expenses.Where(expense => expense.Date >= fromDate && expense.Date <= toDate).ToList();
            return ExpensesList.OrderByDescending(expense => expense.Date).ToList();
        }

        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<ExpenseWithCommentsViewModel>> GetCommentsForExpense(int id)
        {
            var query_v1 = _context.Comment.Where(c => c.Expense.Id == id).Include(c => c.Expense).Select(c => new ExpenseWithCommentsViewModel
            {
                Id = c.Expense.Id,
                Description = c.Expense.Description,
                Sum = c.Expense.Sum,
                Date = c.Expense.Date,
                Type = c.Expense.Type,
                Comments = c.Expense.Comments.Select(exc => new CommentsViewModel
                {
                    Id = exc.Id,
                    Text = exc.Text,
                    Important = exc.Important
                })
            });

            var query_v2 = _context.Expenses.Where(e => e.Id == id).Include(e => e.Comments).Select(e => new ExpenseWithCommentsViewModel
            {
                Id = e.Id,
                Description = e.Description,
                Sum = e.Sum,
                Date = e.Date,
                Type = e.Type,
                Comments = e.Comments.Select(exc => new CommentsViewModel
                {
                    Id = exc.Id,
                    Text = exc.Text,
                    Important = exc.Important
                })
            });
            var query_v3 = _context.Expenses.Where(e => e.Id == id).Include(e => e.Comments).Select(e => _mapper.Map<ExpenseWithCommentsViewModel>(e));

           // var queryForCommentExpenseId = _context.Comment;

           // _logger.LogInformation(queryForCommentExpenseId.ToList()[0].Id.ToString());
          
            _logger.LogInformation(query_v1.ToQueryString());
            _logger.LogInformation(query_v2.ToQueryString());
            _logger.LogInformation(query_v3.ToQueryString());
            return query_v3.ToList();
        }

        [HttpPost("{id}/Comments")]
        public IActionResult PostCommentForExpense(int id, Comment comment)
        {
            var expense = _context.Expenses.Where(e => e.Id == id).Include(e => e.Comments).FirstOrDefault();
            if (expense == null)
            {
                return NotFound();
            }

            expense.Comments.Add(comment);
            _context.Entry(expense).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }
        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseViewModel>> GetExpenses(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }
            var expenseViewModel = _mapper.Map<ExpenseViewModel>(expense);
            return expenseViewModel;
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenses(int id, Expenses expenses)
        {
            if (id != expenses.Id)
            {
                return BadRequest();
            }

            _context.Entry(expenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpensesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expenses>> PostExpenses(Expenses expenses)
        {
            _context.Expenses.Add(expenses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenses", new { id = expenses.Id }, expenses);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenses(int id)
        {
            var cheltuieli = await _context.Expenses.FindAsync(id);
            if (cheltuieli == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(cheltuieli);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpensesExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
