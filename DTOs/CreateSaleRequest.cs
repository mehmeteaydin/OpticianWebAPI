using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticianWebAPI.Models;

namespace OpticianWebAPI.DTOs
{
    public class CreateSaleRequest
    {
        public string CustomerName{get; set;} = string.Empty;
        public string CustomerPhone{get; set;} = string.Empty;

        public Guid FrameId {get; set;}
        
        public string LensBrand{get; set;} = string.Empty;
        public double LensDegreeLeft {get; set;}
        public double LensDegreeRight {get; set;}
        public decimal LensCost {get; set;}

        public GlassesType GlassesType{get; set;}
        public string? Description{get; set;}
    }
}