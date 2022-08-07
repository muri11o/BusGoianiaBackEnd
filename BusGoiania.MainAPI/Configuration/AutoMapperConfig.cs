using AutoMapper;
using BusGoiania.MainAPI.DTOs;
using BusGoiania.MainDomain.Models;

namespace BusGoiania.MainAPI.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<PontoOnibusFavorito, PontoOnibusFavoritoDTO>().ReverseMap();
        }
    }
}
