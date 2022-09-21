using AutoMapper;
using BookMyShowProject.Core.Entities;
using BookMyShowProject.ViewModel;

namespace BookMyShowProject.Configuration
{
    public class AutoMapperProfile : Profile
    {
        internal AutoMapperProfile()
        {
            CreateMap<UserVm, User>();
        }
    }
}
