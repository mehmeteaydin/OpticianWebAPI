using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Services.abstracts;

namespace OpticianWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // Bütün Gider tiplerini getir
        [HttpGet("types")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _expenseService.GetAllExtepnseTypesAsync();
            return Ok(types);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _expenseService.AddExpenseAsync(request);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }


    }
}