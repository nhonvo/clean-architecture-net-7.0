using Api.Core.Entities;
using Api.Core.Models;
using AutoMapper;

namespace Api.ApplicationLogic.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}