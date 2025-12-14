using System;
using OpticianWebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using OpticianWebAPI.Services.abstracts;

namespace OpticianWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GlassesController(IGlassesService glassesService) : ControllerBase
    {
        private readonly IGlassesService _glassesService = glassesService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlassesResponse>>> GetAllGlasses()
        {
            var glassesList = await _glassesService.GetAllGlassesAsync();
            return Ok(glassesList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GlassesResponse>> GetGlassesById(Guid id)
        {
            var glasses = await _glassesService.GetGlassesByIdAsync(id);

            if (glasses == null)
            {
                return NotFound($"ID'si {id} olan gözlük bulunamadı.");
            }

            return Ok(glasses);
        }

        [HttpPost]
        public async Task<ActionResult<GlassesResponse>> Create(CreateGlassesRequest request)
        {
            try
            {
                var createdGlasses = await _glassesService.CreateGlassesAsync(request);
                return CreatedAtAction(nameof(GetGlassesById), new { id = createdGlasses.Id }, createdGlasses);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateGlassesRequest request)
        {
            try
            {
                var isUpdated = await _glassesService.UpdateGlassesAsync(id, request);

                if (!isUpdated)
                {
                    return NotFound($"Güncellenecek ID ({id}) bulunamadı.");
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _glassesService.DeleteGlassesAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Silinecek ID ({id}) bulunamadı.");
            }

            return NoContent();
        }
    }
}