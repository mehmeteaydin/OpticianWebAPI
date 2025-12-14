using System;
using OpticianWebAPI.DTOs;

namespace OpticianWebAPI.Services.abstracts
{
    public interface IGlassesService
    {
        public Task<IEnumerable<GlassesResponse>> GetAllGlassesAsync();
        public Task<GlassesResponse?> GetGlassesByIdAsync(Guid id);
        public Task<GlassesResponse> CreateGlassesAsync(CreateGlassesRequest request);
        public Task<bool> UpdateGlassesAsync(Guid id, UpdateGlassesRequest request);
        public Task<bool> DeleteGlassesAsync(Guid id);
    }
}