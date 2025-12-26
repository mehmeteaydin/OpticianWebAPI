using System;
using AutoMapper;
using OpticianWebAPI.Services.abstracts;
using Microsoft.EntityFrameworkCore;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Models;
using OpticianWebAPI.DatabaseContext;


namespace OpticianWebAPI.Services.concretes
{
    public class FrameService(AppDbContext appDbContext,ILogger<FrameService> logger, IMapper mapper) : IFrameService
    {
        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly ILogger<FrameService> _logger = logger;

        public async Task<FrameResponse> CreateFrameAsync(CreateFrameRequest request)
        {
            var frame = _mapper.Map<Frame>(request);
            frame.Id = Guid.NewGuid();
            frame.CreatedAt = DateTimeOffset.UtcNow;

            await _appDbContext.Frames.AddAsync(frame);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Yeni çerçeve eklendi. Model Kodu: {ModelCode}, Tutar: {Amount}, Renk: {Color}, Materiyal: {Material}, Stok Miktarı: {StockQuantity}, Ekleme Tarihi: {CreatedAt}",
            frame.ModelCode,frame.Cost,frame.Color,frame.Material,frame.StockQuantity,frame.CreatedAt);


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

            _logger.LogInformation("Çerçeve silindi");

            return true;
        }

        public async Task<IEnumerable<FrameResponse>> GetAllFramesAsync()
        {
            var frames = await _appDbContext.Frames.ToListAsync();

            _logger.LogInformation("Bütün çerçeveler getirildi.");

            return _mapper.Map<IEnumerable<FrameResponse>>(frames);
        }

        public async Task<FrameResponse?> GetFrameByIdAsync(Guid id)
        {
            var frame = await _appDbContext.Frames.FindAsync(id);

            if (frame == null)
            {
                return null;
            }

            _logger.LogInformation("İstenilen çerçeve getirildi. Cam No: {Id}",frame.Id);
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

            _logger.LogInformation("Bu ne bilmiyorum ???");

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

            _logger.LogInformation("Çerçeve güncellendi.");
            
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}