using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
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

        
        
        
        public async Task<IEnumerable<MovieDetailes>> GetMovieLanguageGenreAsync(string cityName,string date, string? movieName = null)
        {

           
            var MovieLanguageGenreQuery = "execute  GetMovieLanguageGenre @cityName, @date, @movieName";
            var MovieLanguageGenre = await _dbConnection.QueryAsync<MovieDetailes>(MovieLanguageGenreQuery, new {cityName, date, movieName});
            return MovieLanguageGenre;
        }

        public async Task<IEnumerable<SeatStatus>> GetSeatstatus(int showid)
        {
            var seatStatusQuery = " execute getSeatfiles @showid";
            var searStatus = await _dbConnection.QueryAsync<SeatStatus>(seatStatusQuery, new { showid});
            return searStatus;

        }
    }
}
