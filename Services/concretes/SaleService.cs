using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using OpticianWebAPI.DatabaseContext;
using OpticianWebAPI.DTOs;
using OpticianWebAPI.Models;
using OpticianWebAPI.Services.abstracts;

namespace OpticianWebAPI.Services.concretes
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SaleService> _logger;

        public SaleService(AppDbContext context, ILogger<SaleService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<SaleResponse> MakeSaleAsync(CreateSaleRequest request)
        {
            var frame = _context.Frames.FirstOrDefault(f => f.Id == request.FrameId);

            if (frame == null)
            {
                throw new Exception("Seçilen çerçeve bulunamadı!");
            }

            if (frame.StockQuantity < 1)
            {
                throw new Exception($"'{frame.Brand} {frame.ModelCode}' model çerçeveden stokta kalmamıştır!");
            }

            frame.StockQuantity -= 1;
            frame.UpdatedAt = DateTimeOffset.Now;

            var newLens = new Lens
            {
                Id = Guid.NewGuid(),
                Brand = request.LensBrand,
                Left = request.LensDegreeLeft,
                Right = request.LensDegreeRight,
                Cost = request.LensCost,
                CreatedAt = DateTimeOffset.Now
            };

            var newGlasses = new Glasses
            {
                Id = Guid.NewGuid(),
                FrameId = frame.Id,
                Frame = frame,

                LensId = newLens.Id,
                Lens = newLens,

                Type = request.GlassesType,
                Description = request.Description,
                CreatedAt = DateTimeOffset.Now,
            };

            newGlasses.CalculatePrice();

            var newSale = new Sales
            {
                Id = Guid.NewGuid(),
                CustomerName = request.CustomerName,
                CustomerPhone = request.CustomerPhone,
                SaleDate = DateTimeOffset.Now,
                GlassesId = newGlasses.Id,
                Glasses = newGlasses,
                SoldPrice = newGlasses.TotalPrice
            };

            await _context.Sales.AddAsync(newSale);

            await _context.SaveChangesAsync();

            return new SaleResponse
            {
                SaleId = newSale.Id,
                CustomerName = newSale.CustomerName,
                SaleDate = newSale.SaleDate,
                TotalPrice = newSale.SoldPrice,
                GlassesType = newGlasses.Type.ToString(),
                FrameInfo = $"{frame.Brand} - {frame.ModelCode} ({frame.Color})",
                LensInfo = $"[Sol: {newLens.Left} / Sağ: {newLens.Right}]"
            };
        }
    }
}