using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Services.abstracts;

namespace OpticianWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _saleService.MakeSaleAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleResponse>>> GetSales()
        {
            var responseList = _saleService.GetAllSales();
            return Ok(responseList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleResponse>> GetSaleById([FromBody] Guid id)
        {
            var sale = _saleService.GetSaleAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }
    }
}