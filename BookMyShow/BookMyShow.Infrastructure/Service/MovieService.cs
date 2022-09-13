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
        public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
        {
            return await _movieRepository.GetMoviesAsync();
        }

        // Get movie using id
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _movieRepository.GetMovieAsync(id);
        }



        // Add movie
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            return await _movieRepository.AddMovieAsync(movie);
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

            return await _movieRepository.UpdateMovieAsynce(movieToBeUpdated);

        }

        // delete movie using id
        public async Task DeleteMovieAsync(int id)
        {
            var movie = await GetMovieByIdAsync(id);
            await _movieRepository.DeleteMovieAsync(movie);
        }

        public async Task<IEnumerable<MovieDetailes>> GetMovieByCityNameAsync(string cityName)
        {
            return await _movieRepository.GetMovieCityAsync(cityName);
        }
        public async Task<IEnumerable<MovieDetailes>> GetMovieCityAsync(string cityName, string movieName)
        {
            return await _movieRepository.GetMovieCityAsync(cityName, movieName);

        }

        public async Task<IEnumerable<MovieDetailes>> GetMovieLanguageAsync(string cityName, string? language=null)
        {
            return await _movieRepository.GetMovieLanguageAsync(cityName, language: language);
        }
        public async Task<IEnumerable<MovieDetailes>> GetMovieLanguageGenreAsync(string cityName, string? language=null, string? genre=null)
        {
            return await _movieRepository.GetMovieLanguageGenreAsync(cityName, language: language, genre: genre);
        }
    }
}
