using System;
using OpticianWebAPI.DTOs;

namespace OpticianWebAPI.Services.abstracts
{
    public interface ILensService
    {
        public Task<IEnumerable<LensResponse>> GetAllLensesAsync();
        public Task<LensResponse?> GetLensByIdAsync(Guid id);
        public Task<LensResponse> CreateLensAsync(CreateLensRequest request);
        public Task<LensResponse?> UpdateLensAsync(Guid id, UpdateLensRequest request);
        public Task<bool> DeleteLensAsync(Guid id);
    }
}