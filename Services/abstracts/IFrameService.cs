using System;
using OpticianWebAPI.DTOs;

namespace OpticianWebAPI.Services.abstracts
{
    public interface IFrameService
    {
        public Task<IEnumerable<FrameResponse>> GetAllFramesAsync();
        public Task<FrameResponse?> GetFrameByIdAsync(Guid id);
        public Task<IEnumerable<FrameResponse>> SearchFramesAsync(string searchTerm);
        public Task<FrameResponse> CreateFrameAsync(CreateFrameRequest request);
        public Task<FrameResponse?> UpdateFrameAsync(Guid id, UpdateFrameRequest request);
        public Task<bool> UpdateStockQuantityAsync(Guid id, int changeAmount);
        public Task<bool> DeleteFrameAsync(Guid id); 
    }
}