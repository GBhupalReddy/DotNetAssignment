using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using System.Threading.Tasks;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IMovieRepository
    {
        Task<Movie> AddMovieAsync(Movie movie);
        Task DeleteMovieAsync(Movie movie);
        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<Movie> GetMovieAsync(int id);
        Task<IEnumerable<MovieDetailes>> GetMovieCityAsync(string cityName, string movieName);
        Task<IEnumerable<MovieDetailes>> GetMovieLanguageGenreAsync(string city,string? language=null, string? genre=null);
        Task<IEnumerable<MovieDetailes>> GetMovieLanguageAsync(string city,string? language=null);
        Task<IEnumerable<MovieDetailes>> GetMovieCityAsync(string cityName);
        Task<Movie> UpdateMovieAsynce (Movie movie);
    }
}