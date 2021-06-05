using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laborator1.Data;
using Laborator1.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Laborator1.ViewModels.UserExpensesList;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Laborator1.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application, Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserExpensesListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<UserExpensesListsController> _logger;

        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserExpensesListsController(ApplicationDbContext context, ILogger<UserExpensesListsController> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Add new UserExpensesList
        /// </summary>
        /// <param name="newExpenseRequest"></param>
        /// <returns></returns>
        //POST: api/userExpensesList
        [HttpPost]
        public async Task<ActionResult> MakeNewUserExpensesList(NewExpenseRequest newExpenseRequest)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<Expenses> madeExpenses = new List<Expenses>();
            newExpenseRequest.ExpensesListedIds.ForEach(pid =>
            {
                var expenseWithId = _context.Expenses.Find(pid);
                if (expenseWithId != null)
                {
                    madeExpenses.Add(expenseWithId);
                }
            });

            if (madeExpenses.Count == 0)
            {
                return BadRequest();
            }

            
            var userExpensesList = new UserExpensesList
            {
                ApplicationUser = user,
                OrderDateTime = newExpenseRequest.ExpenseListDateTime.GetValueOrDefault(),
                Expenses = madeExpenses
            };

            _context.UserExpensesLists.Add(userExpensesList);
            await _context.SaveChangesAsync();
            return Ok();
        }

       
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _context.UserExpensesLists.Where(u => u.ApplicationUser.Id == user.Id).Include(u => u.Expenses).FirstOrDefault();
            var resultViewModel = _mapper.Map<ExpensesForUserResponse>(result);

            return Ok(resultViewModel);
        }

       
       
       
       
       /* [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userExpensesList = await _context.UserExpensesLists.FindAsync(id);
            _context.UserExpensesLists.Remove(userExpensesList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExpensesListExists(int id)
        {
            return _context.UserExpensesLists.Any(e => e.Id == id);
        }
*/

        //PUT: api/UserExpensesList
        [HttpPut]
        public async Task<ActionResult> UpdateUserExpensesList(UpdateUserExpensesList updateUserExpensesList)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            UserExpensesList userExpensesList= _context.UserExpensesLists.Where(l => l.Id == updateUserExpensesList.Id && l.ApplicationUser.Id == user.Id).Include(l => l.Expenses).FirstOrDefault();

            if (userExpensesList == null)
            {
                return BadRequest("There is no userExpensesList with this ID.");
            }

            updateUserExpensesList.ExpensesIds.ForEach(mid =>
            {
                var expense = _context.Expenses.Find(mid);
                if (expense != null && !userExpensesList.Expenses.Contains(expense))
                {
                    userExpensesList.Expenses.ToList().Add(expense);
                }
            });


            _context.Entry(userExpensesList).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }
        // DELETE: UserExpensesLists/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserExpensesList(int id)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userExpensesList = _context.UserExpensesLists.Where(u => u.ApplicationUser.Id == user.Id && u.Id == id).FirstOrDefault();

            if (userExpensesList == null)
            {
                return NotFound();
            }

            _context.UserExpensesLists.Remove(userExpensesList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
