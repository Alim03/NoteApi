using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Challenge.Dtos.Note;
using Challenge.Dtos.User;
using Challenge.Models;

namespace Challenge.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserListDto>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<Note, NoteCreateDto>().ReverseMap();
            CreateMap<Note, NoteUpdateDto>().ReverseMap();
        }
    }
}