using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Infrastructure.Repository.EntityFramWork;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BookMyShow.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterSystemServices(this IServiceCollection service)
        {

            // Add services to the container.

            service.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();

        }
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            IDbConnection connection = new SqlConnection(@"Server= (localDb)\MSSQLLocalDB; DataBase=BookMyShow;Trusted_Connection=True;");
            services.AddSingleton<IDbConnection>(connection);
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ICinemaHallRepository, CinemaHallRepository>();
            services.AddTransient<ICinemaRepository, CinemaRepository>();
            services.AddTransient<ICinemaSeatRepository, CinemaSeatRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IShowseatRepository, ShowseatRepository>();
            services.AddTransient<IShowRepository, ShowRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
