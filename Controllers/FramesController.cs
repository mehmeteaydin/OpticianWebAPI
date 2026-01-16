using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Services.abstracts;

namespace OpticianWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    public class FramesController(IFrameService frameService) : ControllerBase
    {
        private readonly IFrameService frameService = frameService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrameResponse>>> GetAllFrames()
        {
            var FrameList = await frameService.GetAllFramesAsync();
            return Ok(FrameList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FrameResponse>> GetFrameById(Guid id)
        {
            var Frame = await frameService.GetFrameByIdAsync(id);
            if (Frame == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrame(Guid id)
        {
            var frame = await frameService.GetFrameByIdAsync(id);

            if (frame == null)
            {
                return NotFound();
            }
            await frameService.DeleteFrameAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFrame(Guid id,[FromBody] UpdateFrameRequest updateFrameRequest)
        {
            await frameService.UpdateFrameAsync(id,updateFrameRequest);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<FrameResponse>> CreateFrame([FromBody] CreateFrameRequest createFrameRequest)
        {
            await frameService.CreateFrameAsync(createFrameRequest);
            return Created("Created",createFrameRequest);
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FrameResponse>>> GetSearchedFrames([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest("Arama bos olamaz");
            }
            var searchedFrames = await frameService.SearchFramesAsync(term);
            return Ok(searchedFrames);
        }


        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> UpdateStockQuantity([FromRoute] Guid id,[FromBody] int amount)
        {
            if(amount < 1) return BadRequest("Miktar 0 dan kucuk olamaz ");

            await frameService.UpdateStockQuantityAsync(id,amount);
            return NoContent();
        }
    }
}