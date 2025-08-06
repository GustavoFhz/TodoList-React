using AutoMapper;
using TodoList.Dto;
using TodoList.Models;

namespace TodoList.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<TarefaCriacaoDto, TarefaModel>().ReverseMap();
        }
    }
}
