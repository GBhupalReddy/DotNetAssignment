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
            var result = (await _dbConnection.QueryAsync<Movie>(query, new { id })).FirstOrDefault();
           // var result = await _dbConnection.QueryFirstAsync<Movie>(query, new { id = id });
            return result;
        }

        public async Task<IEnumerable<MovieDetailes>> GetMovieDetails(string cityName, string movieName)
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
                                where city.CityName == cityName && movie.Tittle == movieName
                                select new MovieDetailes
                                {
                                    MovieName = movie.Tittle,
                                    ShowTiming = show.StartTime,
                                    CinemaName = cinema.CinemaName,
                                    CinemaHallName = cinemaHall.CinemaHallName,
                                    CityName = city.CityName,
                                }).ToListAsync();
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
        public async Task<Movie> UpdateMovieAsynce(int id, Movie movie)
        {
            var movieToBeUpdated = await GetMovieAsync(id);
            movieToBeUpdated.Tittle = movie.Tittle;
            movieToBeUpdated.Description = movie.Description;
            movieToBeUpdated.Duration = movie.Duration;
            movieToBeUpdated.Language = movie.Language;
            movieToBeUpdated.ReleaseDate = movie.ReleaseDate;
            movieToBeUpdated.Country = movie.Country;
            movieToBeUpdated.Genre = movie.Genre;
            _bookMyShowContext.Movies.Update(movieToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return movieToBeUpdated;

        }

        // delete movie using id
        public async Task DeleteMovieAsync(int id)
        {
            var movie = await GetMovieAsync(id);
            _bookMyShowContext.Movies.Remove(movie);
            await _bookMyShowContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieDetailes>> GetMovieDetails(string cityName)
        {
            var result = await(from city in _bookMyShowContext.Cities
                               join cinema in _bookMyShowContext.Cinemas
                               on city.CityId equals cinema.CityId
                               join cinemaHall in _bookMyShowContext.CinemaHalls
                               on cinema.CinemaId equals cinemaHall.CinemaId
                               join show in _bookMyShowContext.Shows
                               on cinemaHall.CinemaHallId equals show.CinemaHallId
                               join movie in _bookMyShowContext.Movies
                               on show.MovieId equals movie.MovieId
                               where city.CityName == cityName
                               select new MovieDetailes
                               {
                                   MovieName = movie.Tittle,
                                   ShowTiming = show.StartTime,
                                   CinemaName = cinema.CinemaName,
                                   CinemaHallName = cinemaHall.CinemaHallName,
                                   CityName = city.CityName,
                               }).ToListAsync();
            return result;
        }
    }
}
