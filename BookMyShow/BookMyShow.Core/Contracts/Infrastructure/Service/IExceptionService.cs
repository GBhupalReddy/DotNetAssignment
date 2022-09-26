namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IExceptionService
    {
        Task<int> VerifyIdExist(int id,string? name = null);
    }
}