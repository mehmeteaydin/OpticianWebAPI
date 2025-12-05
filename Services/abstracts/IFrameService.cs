using System;
using OpticianWebAPI.DTOs;

namespace OpticianWebAPI.Services
{
    public interface IFrameService
    {
        Task<IEnumerable<FrameResponse>> GetAllFramesAsync();
        Task<FrameResponse?> GetFrameByIdAsync(Guid id);
        Task<IEnumerable<FrameResponse>> SearchFramesAsync(string searchTerm);
        Task<FrameResponse> CreateFrameAsync(CreateFrameRequest request);
        Task<FrameResponse?> UpdateFrameAsync(Guid id, UpdateFrameRequest request);
        Task<bool> UpdateStockQuantityAsync(Guid id, int changeAmount);
        Task<bool> DeleteFrameAsync(Guid id); 
    }
}