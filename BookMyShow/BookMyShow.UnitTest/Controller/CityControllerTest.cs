using BookMyShow.Controllers.V1;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.UnitTest.Controller
{
    public class CityControllerTest
    {
        private readonly Mock<ICityService> _cityservicestub;

        public CityControllerTest()
        {
           _cityservicestub = new Mock<ICityService>();
        }

        [Fact]
        public void GetCity_returns_list_of_available_cityes()
        {
            //Arrange
            _cityservicestub
                .Setup(x => x.GetCityByIdAsync(1));
            

            //Act
           var libraryController = new CityController(_cityservicestub.Object);
        }
    }
}
