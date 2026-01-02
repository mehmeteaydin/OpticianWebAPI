using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Models;
using OpticianWebAPI.Services.abstracts;
using OpticianWebAPI.DatabaseContext;
using Microsoft.Extensions.Caching.Memory;

namespace OpticianWebAPI.Services.concretes
{
    public class LensService(AppDbContext appDbContext,ILogger<LensService> logger, IMapper mapper, IMemoryCache cache) : ILensService
    {

        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly ILogger<LensService> _logger = logger;
        private readonly IMemoryCache _cache = cache;
        private readonly string LensCacheKey = "all_lenses_list";
        public async Task<LensResponse> CreateLensAsync(CreateLensRequest request)
        {
            var lens = _mapper.Map<Lens>(request);
            lens.Id = Guid.NewGuid();
            lens.CreatedAt = DateTimeOffset.UtcNow;

            await _appDbContext.Lens.AddAsync(lens);
            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Cam oluşturuldu. Marka: {Brand}, Sol Derece: {Left}, Sağ Derece: {Right}, Tutar: {Cost}, Oluşturulma Tarihi: {CreatedAt}",
            lens.Brand,lens.Left,lens.Right,lens.Cost,lens.CreatedAt);

            _cache.Remove(LensCacheKey);

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

            _cache.Remove(LensCacheKey);

            return true;
        }

        public async Task<IEnumerable<LensResponse>> GetAllLensesAsync()
        {
            if(_cache.TryGetValue(LensCacheKey, out IEnumerable<LensResponse>? cachedList))
            {
                return cachedList!;
            }

            var lenslist = await _appDbContext.Lens.ToListAsync();
            _logger.LogInformation("Bütün lensler getirildi.");

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                Priority = CacheItemPriority.Normal
            };

            _cache.Set(LensCacheKey, lenslist, cacheOptions);

            return _mapper.Map<IEnumerable<LensResponse>>(lenslist);
        }

        public async Task<LensResponse?> GetLensByIdAsync(Guid id)
        {
            if(_cache.TryGetValue(LensCacheKey, out LensResponse? cachedList))
            {
                return cachedList!;
            }
            var lens = await _appDbContext.Lens.FindAsync(id);

            if (lens==null)
                return null;
            
            _logger.LogInformation("İstenilen cam getirildi. Cam No: {Id}, Marka: {Brand}, Tutar: {Cost}",lens.Id, lens.Brand, lens.Cost);

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                Priority = CacheItemPriority.Normal
            };

            _cache.Set(LensCacheKey, lens, cacheOptions);

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

            _cache.Remove(LensCacheKey);

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

            _logger.LogInformation("Cam güncellendi.");

            _cache.Remove(LensCacheKey);

            return true;
        }
    }
}