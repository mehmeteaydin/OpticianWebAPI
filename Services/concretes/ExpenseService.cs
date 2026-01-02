using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;

        private const string ExpensesCacheKey = "all_expenses_list";

        public ExpenseService(AppDbContext context, ILogger<ExpenseService> logger, IMemoryCache cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
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

            _cache.Remove(ExpensesCacheKey);

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

        // Buna gerek kalmadı
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

        public async Task<List<ExpenseResponse>> GetAllExpensesAsync()
        {

            if (_cache.TryGetValue(ExpensesCacheKey, out List<ExpenseResponse>? cachedList))
            {
                return cachedList!;
            }
            var expenses = await _context.Expenses
                                         .OrderByDescending(x => x.ExpenseDate)
                                         .ToListAsync();

            var responseList = expenses.Select(x => new ExpenseResponse
            {
                Id = x.Id,
                Amount = x.Amount,
                Description = x.Description,
                ExpenseDate = x.ExpenseDate,
                TypeId = (int)x.ExpenseType,
                TypeName = x.ExpenseType.ToString()
            }).ToList();

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30), // 30 dakika hafızada tut
                Priority = CacheItemPriority.Normal
            };

            _cache.Set(ExpensesCacheKey, responseList, cacheOptions);

            return responseList;
        }
    }
}