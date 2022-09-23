using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IMovieService
    {
        Task<Movie> AddMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
        Task<Movie> GetMovieByIdAsync(int id);
        Task<IEnumerable<MovieDetailes>> GetMovieLanguageGenreAsync(string cityName, string? language=null, string? genre= null,string ? movieName = null);
        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<Movie> UpdateMovieAsynce(int id, Movie movie);
    }
}