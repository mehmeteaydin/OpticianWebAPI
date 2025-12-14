using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticianWebAPI.DTOs;

namespace OpticianWebAPI.Services.abstracts
{
    public interface IExpenseService
    {
        Task<ExpenseResponse> AddExpenseAsync(CreateExpenseRequest request);

        Task<Dictionary<int, string>> GetAllExtepnseTypesAsync();
    }
}