using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpticianWebAPI.DTOs;

namespace OpticianWebAPI.Services.abstracts
{
    public interface IExpenseService
    {
        Task<ExpenseResponse> AddExpenseAsync(CreateExpenseRequest request);
        public Task<IEnumerable<ExpenseResponse>> GetAllExpensesAsync();
        Task<Dictionary<int, string>> GetAllExtepnseTypesAsync();
    }
}