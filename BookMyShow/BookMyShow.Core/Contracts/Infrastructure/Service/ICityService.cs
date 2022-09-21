using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface ICityService
    {
        Task<City> AddCityAsync(City city);
        Task DeleteCityAsync(int id);
        Task<IEnumerable<CinemaDto>> GetCityInCinemaAsync(string cityName);
        Task<City> GetCityByIdAsync(int id);
        Task<IEnumerable<CityDto>> GetCitysAsync();
        Task<City> UpdateCityAsynce(int id, City city);
        Task<IEnumerable<MovieDto>> GetCityInMovie(string cityName);
        Task<IEnumerable<MovieDetailes>> GetCityCinemaMovieAsync(string cityName, string? cinemaName = null);
    }
}