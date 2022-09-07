using BookMyShow.Core.Dto;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface ICityNametoMovieNameService
    {
        Task<IEnumerable<CinemaDto>> GetCinemasAsync(string cityName);
    }
}