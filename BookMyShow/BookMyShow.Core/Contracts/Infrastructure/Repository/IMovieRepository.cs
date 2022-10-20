using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IMovieRepository
    {
        Task<Movie> AddMovieAsync(Movie movie);
        Task DeleteMovieAsync(Movie movie);
        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<Movie> GetMovieAsync(int id);
        Task<IEnumerable<MovieDetailes>> GetMovieLanguageGenreAsync(string city, string movieName,string date);
        Task<Movie> UpdateMovieAsynce (Movie movie);
        Task<IEnumerable<SeatStatus>> GetSeatstatus(int showid);
    }
}