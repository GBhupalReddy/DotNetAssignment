using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IMovieService
    {
        Task<Movie> AddMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
        Task<Movie> GetMovieByIdAsync(int id);
        Task<IEnumerable<MovieDetailes>> GetMovieLanguageGenreAsync(string cityName, string movieName, string date);
        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<Movie> UpdateMovieAsynce(int id, Movie movie);
        Task<IEnumerable<SeatStatus>> GetSeatstatus(int showid);
    }
}