using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IMovieRepository
    {
        Task<Movie> AddMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
        Task<Movie> GetMovieAsync(int id);
        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<Movie> UpdateMovieAsynce(int id, Movie movie);
    }
}