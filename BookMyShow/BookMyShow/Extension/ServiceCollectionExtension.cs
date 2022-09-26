using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Infrastructure.Configuration;
using BookMyShow.Infrastructure.Data;
using BookMyShow.Infrastructure.Repository.EntityFramWork;
using BookMyShow.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
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
            service.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            service.AddSwaggerGen();
            service.ConfigureOptions<ConfigureSwaggerOptions>();

            service.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

        }
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Repository

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
            services.AddTransient<ISeatTypePriceRepository, SeatTypePriceRepository>();

            //Service

            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<ICinemaHallService, CinemaHallService>();
            services.AddTransient<ICinemaService,CinemaService>();
            services.AddTransient<ICinemaSeatService, CinemaSeatService>();
            services.AddTransient<ICityService,CityService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IShowSeatService, ShowSeatService>();
            services.AddTransient<IShowService, ShowService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISeatTypePriceService, SeatTypePriceService>();
            services.AddTransient<IExceptionService, ExceptionService>();   
            
        }
    }
}
