using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

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

        public async Task<IEnumerable<MovieDetailes>> GetCityInMovieAsync(string cityName, string movieName)
        {
            var result = await _movieRepository.GetCityInMovieAsync(cityName, movieName);
            return result;

        }

        
        public async Task<IEnumerable<MovieDetailes>> GetMovieLanguageGenreAsync(string cityName, string? language=null, string? genre=null,string? movieName=null)
        {
            var result = await _movieRepository.GetMovieLanguageGenreAsync(cityName, language: language, genre: genre, movieName: movieName);
            return result;
        }
    }
}
