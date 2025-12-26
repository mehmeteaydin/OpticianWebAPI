using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticianWebAPI.DatabaseContext;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Models;
using OpticianWebAPI.Services.abstracts;

namespace OpticianWebAPI.Services.concretes
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ExpenseService> _logger;

        public ExpenseService(AppDbContext context, ILogger<ExpenseService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ExpenseResponse> AddExpenseAsync(CreateExpenseRequest request)
        {
            var expense = new Expenses
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                Description = request.Description,
                ExpenseType = request.Type,
                ExpenseDate = DateTimeOffset.Now
            };

            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Yeni gider eklendi. Tutar: {Amount}, Açıklama: {Description}, Tür: {ExpenseType}, Tarih {ExpenseDate}",
             expense.Amount, expense.Description, expense.ExpenseType, expense.ExpenseDate);

            return new ExpenseResponse
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Description = expense.Description,
                ExpenseDate = expense.ExpenseDate,
                TypeId = (int)expense.ExpenseType,
                TypeName = expense.ExpenseType.ToString()
            };


        }

        public Task<Dictionary<int, string>> GetAllExtepnseTypesAsync()
        {
                var types = Enum.GetValues(typeof(ExpensesType))
                .Cast<ExpensesType>()
                .ToDictionary( t =>(int)t,
                t => t.ToString()
                );

                _logger.LogInformation("Bütün giderler getirildi.");

                return Task.FromResult(types);
        }
    }
}