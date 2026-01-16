using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Models;
using OpticianWebAPI.Services.abstracts;

namespace OpticianWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class LensController(ILensService lensService) : ControllerBase
    {
        private readonly ILensService lensService = lensService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LensResponse>>> GetAllLens()
        {
            var lenslist = await lensService.GetAllLensesAsync();
            return Ok(lenslist);
        }

        [HttpPost]
        public async Task<ActionResult<FrameResponse>> CreateLens([FromBody] CreateLensRequest createLensRequest)
        {
            await lensService.CreateLensAsync(createLensRequest);
            return Created("Lens basariyla olusturuldu.",createLensRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLens(Guid id)
        {
            var lens = await lensService.GetLensByIdAsync(id);

            if (lens == null)
            {
                return NotFound();
            }
            await lensService.DeleteLensAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLens(Guid id,UpdateLensRequest updateLensRequest)
        {
            await lensService.UpdateLensAsync(id,updateLensRequest);
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateLensCost(Guid id,decimal newCost)
        {
            if (newCost < 0) return BadRequest("Yeni Fiyat 0'dan kucuk olamaz");
            await lensService.UpdateLensCost(id,newCost);
            return NoContent();
        }
    }
}