using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.Service
{
    public class CinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IDbConnection _dbConnection;
        public CinemaService(ICinemaRepository cinemaRepository, IDbConnection dbConnection)
        {
            _cinemaRepository = cinemaRepository;
            _dbConnection = dbConnection;
        }

        // Get all cinemas
        public async Task<IEnumerable<CinemaDto>> GetCinemasAsync()
        {
            return await _cinemaRepository.GetCinemasAsync();

        }

        // Get cinema using id
        public async Task<Cinema> GetCinemaByIdAsync(int id)
        {
           return await _cinemaRepository.GetCinemaAsync(id);
        }

        //Add cinema
        public async Task<Cinema> AddCinemaAsync(Cinema cinema)
        {
           return await _cinemaRepository.AddCinemaAsync(cinema);
        }

        // update cinema using id
        public async Task<Cinema> UpdateCinemaAsynce(int id, Cinema cinema)
        {
            var CinemaToBeUpdated = await GetCinemaByIdAsync(id);
            CinemaToBeUpdated.CinemaName = cinema.CinemaName;
            CinemaToBeUpdated.TotalCinemaHalls = cinema.TotalCinemaHalls;
            CinemaToBeUpdated.CityId = cinema.CityId;

            return await _cinemaRepository.UpdateCinemaAsynce(id, CinemaToBeUpdated);
            return CinemaToBeUpdated;

        }

        //deleted cinema using id
        public async Task DeleteCinemaAsync(int id)
        {
            await _cinemaRepository.DeleteCinemaAsync(id);
        }
    }
}
