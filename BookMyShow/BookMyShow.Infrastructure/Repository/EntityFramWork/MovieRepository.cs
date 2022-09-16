using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class MovieRepository : IMovieRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public MovieRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        // Get All Movies
        public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
        {
            var query = "select * from Movie";
            var result = await _dbConnection.QueryAsync<MovieDto>(query);
            return result;
            
        }

        // Get movie using id
        public async Task<Movie> GetMovieAsync(int id)
        {
            var query = "select * from Movie where MovieId = @id";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<Movie>(query, new { id }));
            return result;
        }

       

        // Add movie
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _bookMyShowContext.Movies.Add(movie);
            await _bookMyShowContext.SaveChangesAsync();
            return movie;
        }

        // Update movie using id
        public async Task<Movie> UpdateMovieAsynce( Movie movie)
        {
            _bookMyShowContext.Movies.Update(movie);
            await _bookMyShowContext.SaveChangesAsync();
            return movie;

        }

        // delete movie using id
        public async Task DeleteMovieAsync(Movie movie)
        {
            _bookMyShowContext.Movies.Remove(movie);
            await _bookMyShowContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieDetailes>> GetMovieCityAsync(string cityName)
        {
            var result = await (from city in _bookMyShowContext.Cities
                                join cinema in _bookMyShowContext.Cinemas
                                on city.CityId equals cinema.CityId
                                join cinemaHall in _bookMyShowContext.CinemaHalls
                                on cinema.CinemaId equals cinemaHall.CinemaId
                                join show in _bookMyShowContext.Shows
                                on cinemaHall.CinemaHallId equals show.CinemaHallId
                                join movie in _bookMyShowContext.Movies
                                on show.MovieId equals movie.MovieId
                                where city.CityName.ToLower().Contains(cityName.ToLower())
                                select new MovieDetailes
                                {
                                    MovieName = movie.Tittle,
                                    Language = movie.Language,
                                    Genre = movie.Genre,
                                    ShowTiming = show.StartTime,
                                    CinemaName = cinema.CinemaName,
                                    CinemaHallName = cinemaHall.CinemaHallName,
                                    CityName = city.CityName,
                                }).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<MovieDetailes>> GetMovieCityAsync(string cityName, string movieName)
        {
            var cityMovies = await GetMovieCityAsync(cityName);

            var result = from cityMovie in cityMovies
                         where cityMovie.MovieName.ToLower().Contains(movieName.ToLower())
                         select cityMovie;
                                
            return result;

        }
        
        
        public async Task<IEnumerable<MovieDetailes>> GetMovieLanguageGenreAsync(string cityName,string? language=null,string? genre=null, string? movieName = null)
        {

            var result = await (from city in _bookMyShowContext.Cities
                                             join cinema in _bookMyShowContext.Cinemas
                                             on city.CityId equals cinema.CityId
                                             join cinemaHall in _bookMyShowContext.CinemaHalls
                                             on cinema.CinemaId equals cinemaHall.CinemaId
                                             join show in _bookMyShowContext.Shows
                                             on cinemaHall.CinemaHallId equals show.CinemaHallId
                                             join movie in _bookMyShowContext.Movies
                                             on show.MovieId equals movie.MovieId
                                             where city.CityName.ToLower().Contains(cityName.ToLower())
                                             && (string.IsNullOrEmpty(language) || movie.Language.ToLower().Contains(language.ToLower()))
                                             && (string.IsNullOrEmpty(genre) || movie.Genre.ToLower().Contains(genre.ToLower()))
                                             && (string.IsNullOrEmpty(movieName) || movie.Tittle.ToLower().Contains(movieName.ToLower()))
                                select new MovieDetailes
                                             {
                                                 MovieName = movie.Tittle,
                                                 Language = movie.Language,
                                                 Genre = movie.Genre,
                                                 ShowTiming = show.StartTime,
                                                 CinemaName = cinema.CinemaName,
                                                 CinemaHallName = cinemaHall.CinemaHallName,
                                                 CityName = city.CityName,
                                             }).ToListAsync();
            //from cityMovie in cityMovies
            //             where string.IsNullOrEmpty(genre) || cityMovie.Genre.ToLower().Contains(genre.ToLower())
            //             select cityMovie;

            return result;
        }
    }
}
