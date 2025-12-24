using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OpticianWebAPI.DTOs
{
    public class SaleResponse
    {
        public Guid SaleId{get; set;}
        public string CustomerName {get; set;} = string.Empty;
        public DateTimeOffset SaleDate{get; set;}

        public string FrameInfo {get; set;} = string.Empty;
        public string LensInfo {get; set;} = string.Empty;
        public string GlassesType {get; set;} = string.Empty;

        public decimal TotalPrice{get; set;}

     
    }
}