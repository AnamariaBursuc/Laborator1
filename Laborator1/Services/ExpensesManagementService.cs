using Laborator1.Data;
using Laborator1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator1.Services
{
	public class ExpensesManagementService : IExpensesManagementService
	{
		public ApplicationDbContext _context;
		public ExpensesManagementService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<ServiceResponse<List<Expenses>, IEnumerable<EntityManagementError>>> GetExpenses()
		{
			var expenses = await _context.Expenses.ToListAsync();
			var serviceResponse = new ServiceResponse<List<Expenses>, IEnumerable<EntityManagementError>>();
			serviceResponse.ResponseOk = expenses;
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Expenses>, IEnumerable<EntityManagementError>>> GetFilteredExpenses(string startDate, string endDate)
		{
			var startDateDt = DateTime.Parse(startDate);
			var endDateDt = DateTime.Parse(endDate);

			var expenses = await _context.Expenses.Where(m => m.Date >= startDateDt && m.Date <= endDateDt)
				.ToListAsync();

			var serviceResponse = new ServiceResponse<List<Expenses>, IEnumerable<EntityManagementError>>();
			serviceResponse.ResponseOk = expenses;
			return serviceResponse;
		}

		public async Task<ServiceResponse<Expenses, IEnumerable<EntityManagementError>>> GetExpenses(int id)
		{
			var expenses = await _context.Expenses.FindAsync(id);

			var serviceResponse = new ServiceResponse<Expenses, IEnumerable<EntityManagementError>>();
			serviceResponse.ResponseOk = expenses;
			return serviceResponse;
		}

		public async Task<ServiceResponse<List<Comment>, IEnumerable<EntityManagementError>>> GetCommentsForExpense(int id)
		{
			var comments = await _context.Comment.Where(c => c.ExpenseId == id).ToListAsync();

			var serviceResponse = new ServiceResponse<List<Comment>, IEnumerable<EntityManagementError>>();
			serviceResponse.ResponseOk = comments;
			return serviceResponse;
		}

		public async Task<ServiceResponse<Expenses, IEnumerable<EntityManagementError>>> UpdateExpenses(Expenses expenses)
		{
			_context.Entry(expenses).State = EntityState.Modified;
			var serviceResponse = new ServiceResponse<Expenses, IEnumerable<EntityManagementError>>();

			try
			{
				await _context.SaveChangesAsync();
				serviceResponse.ResponseOk = expenses;
			}
			catch (DbUpdateConcurrencyException e)
			{
				var errors = new List<EntityManagementError>();
				errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<Comment, IEnumerable<EntityManagementError>>> UpdateComment(Comment comment)
		{
			_context.Entry(comment).State = EntityState.Modified;
			var serviceResponse = new ServiceResponse<Comment, IEnumerable<EntityManagementError>>();

			try
			{
				await _context.SaveChangesAsync();

				serviceResponse.ResponseOk = comment;
			}
			catch (DbUpdateConcurrencyException e)
			{
				var errors = new List<EntityManagementError>();
				errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<Expenses, IEnumerable<EntityManagementError>>> addExpense (Expenses expenses)
		{
			_context.Expenses.Add(expenses);
			var serviceResponse = new ServiceResponse<Expenses, IEnumerable<EntityManagementError>>();

			try
			{
				await _context.SaveChangesAsync();
				serviceResponse.ResponseOk = expenses;
			}
			catch (Exception e)
			{
				var errors = new List<EntityManagementError>();
				errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
			}

			return serviceResponse;
		}
		public async Task<ServiceResponse<Comment, IEnumerable<EntityManagementError>>> AddComment(Comment comment)
		{
			_context.Comment.Add(comment);
			var serviceResponse = new ServiceResponse<Comment, IEnumerable<EntityManagementError>>();

			try
			{
				await _context.SaveChangesAsync();
				serviceResponse.ResponseOk = comment;
			}
			catch (Exception e)
			{
				var errors = new List<EntityManagementError>();
				errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<Comment, IEnumerable<EntityManagementError>>> AddCommentToExpenses(int expenseId, Comment comment)
		{
			var expenses = await _context.Expenses
				.Where(m => m.Id == expenseId)
				.Include(m => m.Comments).FirstOrDefaultAsync();

			var serviceResponse = new ServiceResponse<Comment, IEnumerable<EntityManagementError>>();

			if (expenses == null)
			{
				var errors = new List<EntityManagementError>();
				errors.Add(new EntityManagementError { Description = "The expense doesn't exist." });
				return serviceResponse;
			}

			try
			{
				expenses.Comments.Add(comment);
				_context.Entry(expenses).State = EntityState.Modified;
				_context.SaveChanges();

				serviceResponse.ResponseOk = comment;
			}
			catch (Exception e)
			{
				var errors = new List<EntityManagementError>();
				errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<bool, IEnumerable<EntityManagementError>>> DeleteComment(int commentId)
		{
			var serviceResponse = new ServiceResponse<bool, IEnumerable<EntityManagementError>>();

			try
			{
				var comment = await _context.Comment.FindAsync(commentId);
				_context.Comment.Remove(comment);
				await _context.SaveChangesAsync();
				serviceResponse.ResponseOk = true;
			}
			catch (Exception e)
			{
				var errors = new List<EntityManagementError>();
				errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<bool, IEnumerable<EntityManagementError>>> DeleteExpenses(int expenseId)
		{
			var serviceResponse = new ServiceResponse<bool, IEnumerable<EntityManagementError>>();

			try
			{
				var expenses = await _context.Expenses.FindAsync(expenseId);
				_context.Expenses.Remove(expenses);
				await _context.SaveChangesAsync();
				serviceResponse.ResponseOk = true;
			}
			catch (Exception e)
			{
				var errors = new List<EntityManagementError>();
				errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
			}

			return serviceResponse;
		}

		public bool ExpensesExists(int id)
		{
			return _context.Expenses.Any(e => e.Id == id);
		}

		public bool CommentExists(int id)
		{
			return _context.Expenses.Any(e => e.Id == id);
		}
	}
}

