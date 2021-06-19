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

        /// <summary>
        /// Retrieves a list of expense filtered by the minSum when added
        /// </summary>
        /// remarks
        /// sample request:
        /// https://localhost:44371/api/expenses/5
        /// <param name="minSum"></param>
        /// <returns> A list of expenses with minimum sum introduced</returns>
        /// <response code="200" >Returns the expenses filtered</response>
        ///  <response code="400" >If the item is null</response>
        [HttpGet]
        [Route("filter/{minSum}")]
        public ActionResult<IEnumerable<Expenses>> FilterExpenses(int minSum)
        {
            var query = _context.Expenses.Where(e => e.Sum >= minSum);
            _logger.LogInformation(query.ToQueryString());
            return query.ToList();
        }

        /// <summary>
        /// Retrieves all the expenses
        /// </summary>
        /// sample request:
        /// https://localhost:44371/api/expenses
        /// <param name="minSum"></param>
        /// <returns></returns>
        // GET: api/Expenses   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expenses>>> GetExpenses()
        
            {
                return await _context.Expenses.ToListAsync();
            }
      

        /// <summary>
        /// Retrieves a list of expense filtered by the dates added
        /// </summary>
        /// remarks
        /// sample request:
        /// https://localhost:44371/api/expenses/filter?fromDate=2019-03-10T00:00:00&toDate=2019-03-11T00:00:00
        /// <param name="fromDate">Date of begining for the interval</param>
        ///    /// <param name="toDate">Date of ending for the interval</param>
        /// <returns> A list of expenses with minimum sum introduced</returns>
        /// <response code="200" >Returns the expenses filtered</response>
        ///  <response code="400" >If the item is null</response>
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

        /// <summary>
        /// It retrieves an expense by id with the comments included
        /// </summary>
        /// sample request:
        ///https://localhost:44371/api/expenses/6/comments
        /// <param name="id"></param>
        /// <response code="200" >The expense is shown</response>
        /// <response code="404" >If the item is null</response>
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
        /// <summary>
        /// Posts a comment to an expense based on the expense id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost("{id}/Comments")]
        public IActionResult PostCommentForExpense(int id, CommentsViewModel comment)
        {
            var expense = _context.Expenses.Where(e => e.Id == id).Include(e => e.Comments).FirstOrDefault();
            if (expense == null)
            {
                return NotFound();
            }

            expense.Comments.Add(_mapper.Map<Comment>(comment));
            _context.Entry(expense).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Retrieves expense by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the expense by the id introduced</returns>
        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseViewModel>> GetExpenses(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }
            return _mapper.Map<ExpenseViewModel>(expense);
         
        }
        /// <summary>
        /// Updates an expense by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expenses"></param>
        /// <returns>A newly created expense</returns>
        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenses(int id, Expenses expenses)
        {
            if (id != expenses.Id)
            {
                return BadRequest();
            }
            

            _context.Entry(_mapper.Map<Expenses>(expenses)).State = EntityState.Modified;

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
        /// <summary>
        /// Ads a new expense
        /// </summary>
        /// sample request:
        /// {
        ///  "description": "Apa",
        /// "sum": 200, 
        /// "type":1,
        ///"date": "2019-03-13T11:30:00"

        ///}

        /// <param name="expenses"></param>
      
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseViewModel>> PostExpenses(ExpenseViewModel expensesRequest)
        {
            Expenses expenses = _mapper.Map<Expenses>(expensesRequest);
            _context.Expenses.Add(expenses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenses", new { id = expenses.Id }, expenses);
        }
        /// <summary>
        /// Deletes an expense by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenses(int id)
        {
            var expenses = await _context.Expenses.FindAsync(id);
            if (expenses== null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expenses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpensesExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
