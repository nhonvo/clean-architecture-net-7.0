using Api.Core.Entities;
using AutoMapper;
using Models.Book;
using Models.User;

namespace Api.ApplicationLogic.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();

            CreateMap<User, LoginRequest>().ReverseMap();
            CreateMap<User, RegisterRequest>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}