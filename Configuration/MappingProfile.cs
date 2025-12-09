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
        // --- Frame Mapping ---
        CreateMap<CreateFrameRequest, Frame>();
        CreateMap<UpdateFrameRequest, Frame>();
        CreateMap<Frame, FrameResponse>();

        // --- Lens Mapping ---
        CreateMap<CreateLensRequest, Lens>();
        CreateMap<UpdateLensRequest, Lens>();
        CreateMap<Lens, LensResponse>();

        // --- Glasses Mapping ---
        CreateMap<CreateGlassesRequest, Glasses>();
        CreateMap<UpdateGlassessRequest, Glasses>();
        CreateMap<Glasses, GlassessResponse>();
    }   
    }
}