using System;
using AutoMapper;
using OpticianWebAPI.Services.abstracts;
using Microsoft.EntityFrameworkCore;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Models;


namespace OpticianWebAPI.Services.concretes
{
    public class FrameService(AppDbContext appDbContext, IMapper mapper) : IFrameService
    {
        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<FrameResponse> CreateFrameAsync(CreateFrameRequest request)
        {
            var frame = _mapper.Map<Frame>(request);
            frame.Id = Guid.NewGuid();
            frame.CreatedAt = DateTimeOffset.UtcNow;

            await _appDbContext.Frames.AddAsync(frame);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<FrameResponse>(frame);  
        }

        public async Task<bool> DeleteFrameAsync(Guid id)
        {
            var frame = await _appDbContext.Frames.FindAsync(id);

            if (frame == null)
            {
                return false;
            }

            _appDbContext.Frames.Remove(frame);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<FrameResponse>> GetAllFramesAsync()
        {
            var frames = await _appDbContext.Frames.ToListAsync();
            return _mapper.Map<IEnumerable<FrameResponse>>(frames);
        }

        public async Task<FrameResponse?> GetFrameByIdAsync(Guid id)
        {
            var frame = await _appDbContext.Frames.FindAsync(id);

            if (frame == null)
            {
                return null;
            }
            return _mapper.Map<FrameResponse>(frame);
        }

        public async Task<IEnumerable<FrameResponse>> SearchFramesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<FrameResponse>();
            }

            var term = searchTerm.Trim().ToLower();
            var frames = await _appDbContext.Frames
                .Where(f => 
                    f.Brand.ToLower().Contains(term) || 
                    
                    f.ModelCode.ToLower().Contains(term) ||
                    
                    (f.Color != null && f.Color.ToLower().Contains(term))
                )
                .ToListAsync();

            return _mapper.Map<IEnumerable<FrameResponse>>(frames);
        }

        public async Task<FrameResponse?> UpdateFrameAsync(Guid id, UpdateFrameRequest request)
        {
            var existingFrame = await _appDbContext.Frames.FindAsync(id);

            if (existingFrame == null)
            {
                return null;
            }

            _mapper.Map(request,existingFrame);
            existingFrame.UpdatedAt = DateTimeOffset.UtcNow;

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<FrameResponse>(existingFrame);
        }

        public async Task<bool> UpdateStockQuantityAsync(Guid id, int changeAmount)
        {
            var existingFrame = await _appDbContext.Frames.FindAsync(id);

            if (existingFrame == null)
                return false;
            if (changeAmount == 0)
                return false;
            if (existingFrame.StockQuantity + changeAmount < 0)
                return false;

            existingFrame.UpdatedAt = DateTimeOffset.UtcNow;
            existingFrame.StockQuantity += changeAmount;
            
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}