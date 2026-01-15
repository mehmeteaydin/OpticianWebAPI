using System;
using AutoMapper;
using OpticianWebAPI.Models;
using OpticianWebAPI.DTOs;


namespace OpticianWebAPI.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateFrameRequest, Frame>();
            CreateMap<UpdateFrameRequest, Frame>();
            CreateMap<Frame, FrameResponse>();

            CreateMap<CreateLensRequest, Lens>();
            CreateMap<UpdateLensRequest, Lens>();
            CreateMap<Lens, LensResponse>();

            CreateMap<CreateGlassesRequest, Glasses>();
            CreateMap<UpdateGlassesRequest, Glasses>();
            CreateMap<Glasses, GlassesResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<CreateExpenseRequest , Expenses>();
            CreateMap<Expenses,ExpenseResponse>();

            CreateMap<CreateSaleRequest, Sales>();
            CreateMap<Sales,SaleResponse>();          
        }   
    }
}