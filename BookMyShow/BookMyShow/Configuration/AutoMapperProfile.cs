using AutoMapper;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;

namespace BookMyShow.Configuration
{
    internal class AutoMapperProfile : Profile
    {
        internal AutoMapperProfile()
        {
            CreateMap<UserVm, User>();
            CreateMap<User, UserDto>();
        }
    }
}
