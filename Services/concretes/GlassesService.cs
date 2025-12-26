using System;
using OpticianWebAPI.Services.abstracts;
using OpticianWebAPI.DatabaseContext;
using OpticianWebAPI.DTOs;
using AutoMapper;
using OpticianWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OpticianWebAPI.Services.concretes
{
    public class GlassesService(AppDbContext appDbContext, ILogger<GlassesService> logger, IMapper mapper) : IGlassesService
    {
        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly ILogger<GlassesService> _logger = logger;
        
        public async Task<IEnumerable<GlassesResponse>> GetAllGlassesAsync()
        {
            var glassesList = await _appDbContext.Glasses
                .Include(g => g.Frame)
                .Include(g => g.Lens)
                .ToListAsync();

            _logger.LogInformation("Bütün gözlükler getirildi");
            return _mapper.Map<IEnumerable<GlassesResponse>>(glassesList);
        }

        public async Task<GlassesResponse?> GetGlassesByIdAsync(Guid id)
        {
            var glasses = await _appDbContext.Glasses
                .Include(g => g.Frame)
                .Include(g => g.Lens)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (glasses == null) return null;

            _logger.LogInformation("Gözlük getirildi. Gözlük Tipi: {Type}, Cam: {Lens}, Çerçeve: {Frame}, Fiyat: {TotalPrice}, Açıklama: {Description}, Oluşturulma Tarihi: {CreatedAt}",
            glasses.Type,glasses.Lens,glasses.Frame,glasses.TotalPrice,glasses.Description,glasses.CreatedAt);

            return _mapper.Map<GlassesResponse>(glasses);
        }

        public async Task<GlassesResponse> CreateGlassesAsync(CreateGlassesRequest request)
        {
            var frame = await _appDbContext.Frames.FindAsync(request.FrameId);
            var lens = await _appDbContext.Lens.FindAsync(request.LensId);

            if (frame == null || lens == null)
                throw new InvalidOperationException("Geçersiz Frame veya Lens ID'si.");

            var glasses = _mapper.Map<Glasses>(request);
            
            glasses.Id = Guid.NewGuid();
            glasses.Frame = frame; 
            glasses.Lens = lens;
            glasses.CreatedAt = DateTimeOffset.UtcNow;
            glasses.CalculatePrice();

            await _appDbContext.Glasses.AddAsync(glasses);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Gözlük oluşturuldu. Çerçeve: {Frame}, Cam: {Lens}, Tutar: {TotalPrice}, Oluşturulma Tarihi: {CreatedAt}",
            glasses.Frame,glasses.Lens,glasses.TotalPrice,glasses.CreatedAt);

            return _mapper.Map<GlassesResponse>(glasses);
        }

        public async Task<bool> UpdateGlassesAsync(Guid id, UpdateGlassesRequest request)
        {
            var existingGlasses = await _appDbContext.Glasses
                .Include(g => g.Frame)
                .Include(g => g.Lens)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (existingGlasses == null) return false;

            var frame = await _appDbContext.Frames.FindAsync(request.FrameId);
            var lens = await _appDbContext.Lens.FindAsync(request.LensId);

            if (frame == null || lens == null)
                throw new InvalidOperationException("Geçersiz Frame veya Lens ID'si.");

            _mapper.Map(request, existingGlasses);

            existingGlasses.Frame = frame;
            existingGlasses.Lens = lens;
            existingGlasses.UpdatedAt = DateTimeOffset.UtcNow;

            existingGlasses.CalculatePrice();

            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Gözlük güncellendi. Çerçeve: {Frame}, Cam: {Lens}, Tutar: {TotalPrice}, Güncellenme Tarihi: {UpdateAt}",
            existingGlasses.Frame,existingGlasses.Lens,existingGlasses.TotalPrice,existingGlasses.UpdatedAt);

            return true;
        }

        public async Task<bool> DeleteGlassesAsync(Guid id)
        {
            var glasses = await _appDbContext.Glasses.FindAsync(id);
            if (glasses == null) return false;

            _appDbContext.Glasses.Remove(glasses);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Gözlük silindi. Gözlük No: {Id}",glasses.Id);
            
            return true;
        }
    }
}