using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            var query = "select * from Movie";
            var result = await _dbConnection.QueryAsync<Movie>(query);
            return result;

        }

        // Get movie using id
        public async Task<Movie> GetMovieAsync(int id)
        {
            var query = "select * from Movie where MovieId = @id";
            var result = await _dbConnection.QueryFirstAsync<Movie>(query, new { id = id });
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
    }
}
