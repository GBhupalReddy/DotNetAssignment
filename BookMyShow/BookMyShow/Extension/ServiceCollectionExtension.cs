using BookMyShow.Configuration;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Infrastructure.Data;
using BookMyShow.Infrastructure.Repository.EntityFramWork;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMyShow.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterSystemServices(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddDbContext<BookMyShowContext>(data =>
            {
                data.UseSqlServer(configuration.GetConnectionString("BookMyShowConnection"));
            });
            service.AddTransient<IDbConnection>(db => new SqlConnection(
                                configuration.GetConnectionString("BookMyShowConnection")));
            // Add services to the container.

            service.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();

            

        }
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
           

            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ICinemaHallRepository, CinemaHallRepository>();
            services.AddTransient<ICinemaRepository, CinemaRepository>();
            services.AddTransient<ICinemaSeatRepository, CinemaSeatRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IShowSeatRepository, ShowSeatRepository>();
            services.AddTransient<IShowRepository, ShowRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
