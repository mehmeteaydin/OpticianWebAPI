using System;
using OpticianWebAPI.DTOs;

namespace OpticianWebAPI.Services.concretes
{
    public class FrameService : IFrameService
    {
        Task<FrameResponse> IFrameService.CreateFrameAsync(CreateFrameRequest request)
        {
            return Task.FromResult(new FrameResponse());       
        }

        Task<bool> IFrameService.DeleteFrameAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<FrameResponse>> IFrameService.GetAllFramesAsync()
        {
            throw new NotImplementedException();
        }

        Task<FrameResponse?> IFrameService.GetFrameByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<FrameResponse>> IFrameService.SearchFramesAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        Task<FrameResponse?> IFrameService.UpdateFrameAsync(Guid id, UpdateFrameRequest request)
        {
            throw new NotImplementedException();
        }

        Task<bool> IFrameService.UpdateStockQuantityAsync(Guid id, int changeAmount)
        {
            throw new NotImplementedException();
        }
    }
}