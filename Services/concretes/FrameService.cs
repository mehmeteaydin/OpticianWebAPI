using System;
using AutoMapper;
using AutoMapper;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Models;
using OpticianWebAPI.Models;

namespace OpticianWebAPI.Services.concretes
{
    public class FrameService(IMapper mapper) : IFrameService
    public class FrameService(IMapper mapper) : IFrameService
    {
        private readonly IMapper _mapper = mapper;
        private static List<Frame> frames = new()
        {
            new Frame
    {
        Id = Guid.NewGuid(),
        Brand = "Ray-Ban",
        ModelCode = "RB3447 Round Metal",
        Cost = 3500.50m,
        Color = "Altın",
        Material = "Metal",
        StockQuantity = 15,
        CreatedAt = DateTimeOffset.UtcNow.AddDays(-30),
        UpdatedAt = null
    },
    new Frame
    {
        Id = Guid.NewGuid(),
        Brand = "Oakley",
        ModelCode = "OX8156 Holbrook",
        Cost = 2800.00m,
        Color = "Mat Siyah",
        Material = "O-Matter", // Oakley'in özel plastik materyali
        StockQuantity = 8,
        CreatedAt = DateTimeOffset.UtcNow.AddDays(-15),
        UpdatedAt = null
    },
    new Frame
    {
        Id = Guid.NewGuid(),
        Brand = "Prada",
        ModelCode = "PR 17WS",
        Cost = 6200.00m,
        Color = "Kaplumbağa", // Tortoise
        Material = "Asetat",
        StockQuantity = 4,
        CreatedAt = DateTimeOffset.UtcNow.AddDays(-60),
        UpdatedAt = DateTimeOffset.UtcNow.AddDays(-2) // Yakın zamanda güncellenmiş
    },
    new Frame
    {
        Id = Guid.NewGuid(),
        Brand = "Tom Ford",
        ModelCode = "TF5555-B",
        Cost = 7500.00m,
        Color = "Lacivert",
        Material = "Titanyum",
        StockQuantity = 6,
        CreatedAt = DateTimeOffset.UtcNow.AddDays(-10),
        UpdatedAt = null
    }
        };
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
            var responseList = _mapper.Map<IEnumerable<FrameResponse>>(frames);
            return Task.FromResult(responseList); 
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