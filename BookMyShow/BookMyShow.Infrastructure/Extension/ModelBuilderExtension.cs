using BookMyShow.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.Extension
{
    internal static class ModelBuilderExtension
    {
        internal static void RegisterEntityConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookingEntityTypeConfiguarction());
            modelBuilder.ApplyConfiguration(new CinemaEntityTypeConfiguarction());
            modelBuilder.ApplyConfiguration(new CinemaHallEntityTypeConfiguarction());
            modelBuilder.ApplyConfiguration(new CinemaSeatEntityTypeConfiguarction());
            modelBuilder.ApplyConfiguration(new CityEntityTypeConfiguarction());
            modelBuilder.ApplyConfiguration(new MovieEntityTypeConfiguarction());
            modelBuilder.ApplyConfiguration(new PaymentEntityTypeConfiguarction());
            modelBuilder.ApplyConfiguration(new ShowEntityTypeConfiguarction());
            modelBuilder.ApplyConfiguration(new ShowSeatEntityTypeConfiguarction());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguarction());
        }
    }
}
