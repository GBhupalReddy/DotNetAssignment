using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Exceptions;

namespace BookMyShow.Infrastructure.Service
{
    public class ExceptionService : IExceptionService
    {
        public Task<int> VerifyIdExist(int id,string? name = null)
        {
            if (id <= 0)
            {
                throw id switch
                {
                    _ => new AppException("Input request is invalid")
                };
            }
            throw id switch
            {

                _ => new DuplicateException($"{name} exits ", new Exception($"{name} not existed with this  Id :  {id} "))
            };
        }
        
    }
}
