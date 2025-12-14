using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticianWebAPI.DTOs;

namespace OpticianWebAPI.Services.abstracts
{
    public interface ISaleService
    {
        Task<SaleResponse> MakeSale(CreateSaleRequest request);
    }
}