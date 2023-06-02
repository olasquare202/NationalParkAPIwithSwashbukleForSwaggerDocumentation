using AutoMapper;
using NationalParkAPI.Dtos;
using NationalParkAPI.Models;
using NationalParkAPI.Models.Dtos;

namespace NationalParkAPI.Mapper
{
    public class NationalParkMappings : Profile //inherite from Profile wc is found in using AutoMapper name space
    {
        //We do all d mappings in d constructor below
        public NationalParkMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap();
        }
    }
}
