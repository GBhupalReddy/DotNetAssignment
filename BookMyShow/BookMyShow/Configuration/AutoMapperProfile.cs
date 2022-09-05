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
            CreateMap<BookingVm, Booking>();
            CreateMap<CinemaHallVm, CinemaHall>();
            CreateMap<CinemaVm, Cinema>();
            CreateMap<CinemaSeatVm, CinemaSeat>();
            CreateMap<CityVm, City>();
            CreateMap<MovieVm, Movie>();
            CreateMap<PaymentVm, Payment>();
            CreateMap<ShowSeatVm, ShowSeat>();
            CreateMap<ShowVm, Show>();
            CreateMap<UserVm, User>();
            CreateMap<User, UserDto>();
        }
    }
}
