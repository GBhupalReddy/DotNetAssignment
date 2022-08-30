using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class MovieRepository : IMovieRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        public MovieRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }


        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await (from movie in _bookMyShowContext.Movies

                          select new Movie
                          {
                              MovieId = movie.MovieId,
                              Tittle = movie.Tittle,
                              Description = movie.Description,
                              Duration = movie.Duration,
                              Language = movie.Language,
                              ReleaseDate = movie.ReleaseDate,
                              Country = movie.Country,
                              Genre = movie.Genre,

                          }).ToListAsync();

        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            return await _bookMyShowContext.Movies.FindAsync(id);
        }

        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _bookMyShowContext.Movies.Add(movie);
            await _bookMyShowContext.SaveChangesAsync();
            return movie;
        }
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

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await GetMovieAsync(id);
            _bookMyShowContext.Movies.Remove(movie);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
