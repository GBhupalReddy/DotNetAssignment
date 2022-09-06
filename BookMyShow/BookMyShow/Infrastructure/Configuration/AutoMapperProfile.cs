using AutoMapper;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;

namespace BookMyShow.Infrastructure.Configuration
{
    internal class AutoMapperProfile : Profile
    {
        internal AutoMapperProfile()
        {
            // viewModel to entities
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

            // entities to Dto

            CreateMap<Booking, BookingDto>();
            CreateMap<CinemaHall, CinemaHallDto>();
            CreateMap<Cinema, CinemaDto>();
            CreateMap<CinemaSeat, CinemaSeatDto>();
            CreateMap<City, CityDto>();
            CreateMap<Movie, MovieDto>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<ShowSeat, ShowSeatDto>();
            CreateMap<Show, ShowDto>();
            CreateMap<User, UserDto>();
        }
    }
}
