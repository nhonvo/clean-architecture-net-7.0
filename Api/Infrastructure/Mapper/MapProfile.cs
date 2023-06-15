using Api.ApplicationLogic.Services;
using Api.Core.Entities;
using AutoMapper;

namespace Api.Infrastructure.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}