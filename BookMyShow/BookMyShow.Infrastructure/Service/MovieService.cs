using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using NPOI.SS.Formula.Functions;

namespace BookMyShow.Infrastructure.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // Get Movies
        public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return movies;
        }

        // Get movie using id
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);
            return movie;
        }



        // Add movie
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            var reusult = await _movieRepository.AddMovieAsync(movie);
            return reusult;
        }

        // Update movie using id
        public async Task<Movie> UpdateMovieAsynce(int id, Movie movie)
        {
            var movieToBeUpdated = await GetMovieByIdAsync(id);
            movieToBeUpdated.Tittle = movie.Tittle;
            movieToBeUpdated.Description = movie.Description;
            movieToBeUpdated.Duration = movie.Duration;
            movieToBeUpdated.Language = movie.Language;
            movieToBeUpdated.ReleaseDate = movie.ReleaseDate;
            movieToBeUpdated.Country = movie.Country;
            movieToBeUpdated.Genre = movie.Genre;

            var reusult = await _movieRepository.UpdateMovieAsynce(movieToBeUpdated);
            return reusult;

        }

        // delete movie using id
        public async Task DeleteMovieAsync(int id)
        {
            var movie = await GetMovieByIdAsync(id);
            await _movieRepository.DeleteMovieAsync(movie);
        }



        public async Task<IEnumerable<MovieDetailes>> GetMovieLanguageGenreAsync(string cityName, string? date = null, string? movieName = null)
        {
            if (date == null)
                date = DateTime.Now.ToString("yyyy-MM-dd");
            var result = await _movieRepository.GetMovieLanguageGenreAsync(cityName, date, movieName: movieName);
            return result;
        }

        public async Task<IEnumerable<SeatStatus>> GetSeatstatus(int showid)
        {
            var searStatus = await _movieRepository.GetSeatstatus(showid);
            return searStatus;

        }
    }
}
