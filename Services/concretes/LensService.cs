using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Models;
using OpticianWebAPI.Services.abstracts;
using OpticianWebAPI.DatabaseContext;

namespace OpticianWebAPI.Services.concretes
{
    public class LensService(AppDbContext appDbContext,ILogger<LensService> logger, IMapper mapper) : ILensService
    {

        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly ILogger<LensService> _logger = logger;
        public async Task<LensResponse> CreateLensAsync(CreateLensRequest request)
        {
            var lens = _mapper.Map<Lens>(request);
            lens.Id = Guid.NewGuid();
            lens.CreatedAt = DateTimeOffset.UtcNow;

            await _appDbContext.Lens.AddAsync(lens);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Cam oluşturuldu. Marka: {Brand}, Sol Derece: {Left}, Sağ Derece: {Right}, Tutar: {Cost}, Oluşturulma Tarihi: {CreatedAt}",
            lens.Brand,lens.Left,lens.Right,lens.Cost,lens.CreatedAt);

            return _mapper.Map<LensResponse>(lens); 
        }

        public async Task<bool> DeleteLensAsync(Guid id)
        {
            var lens = await _appDbContext.Lens.FindAsync(id);

            if (lens == null)
            {
                return false;
            }

            _appDbContext.Lens.Remove(lens);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Cam silindi. Lens No: {Id}",lens.Id);

            return true;
        }

        public async Task<IEnumerable<LensResponse>> GetAllLensesAsync()
        {
            var lenslist = await _appDbContext.Lens.ToListAsync();

            _logger.LogInformation("Bütün lensler getirildi.");

            return _mapper.Map<IEnumerable<LensResponse>>(lenslist);
        }

        public async Task<LensResponse?> GetLensByIdAsync(Guid id)
        {
            var lens = await _appDbContext.Lens.FindAsync(id);

            if (lens==null)
                return null;

            _logger.LogInformation("İstenilen cam getirildi. Cam No: {Id}, Marka: {Brand}, Tutar: {Cost}",lens.Id, lens.Brand, lens.Cost);
            return _mapper.Map<LensResponse>(lens);
        }

        public async Task<LensResponse?> UpdateLensAsync(Guid id, UpdateLensRequest request)
        {
            var existingLens = await _appDbContext.Lens.FindAsync(id);

            if (existingLens == null)
            {
                return null;
            }

            _mapper.Map(request,existingLens);
            existingLens.UpdatedAt = DateTimeOffset.UtcNow;

            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Lens güncellendi.");

            return _mapper.Map<LensResponse>(existingLens);
        }

        public async Task<bool> UpdateLensCost(Guid id,decimal newCost)
        {
            var existingLens = await _appDbContext.Lens.FindAsync(id);
            
            if(existingLens == null)
                return false;
            if(newCost < 0)
                return false;
            
            existingLens.UpdatedAt = DateTimeOffset.UtcNow;
            existingLens.Cost = newCost;

            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Cam tutarı güncellendi.");

            return true;
        }
    }
}