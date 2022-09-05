﻿using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IShowRepository
    {
        Task<Show> AddShowAsync(Show show);
        Task DeleteShowAsync(int id);
        Task<Show> GetShowAsync(int id);
        Task<IEnumerable<Show>> GetShowsAsync();
        Task<Show> UpdateShowAsynce(int id, Show show);
    }
}