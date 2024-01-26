using Countries.Controllers;
using Countries.Interfaces;
using Countries.Models;
using Countries.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.UnitTest
{
    /// <summary>
    /// Just for example....
    /// Because CountryAPIController uses ICountryService which can acts on different kinds of datasources, 
    /// I used MOCK
    /// </summary>
    [TestClass]
    public class CountryAPIControllerTests
    {
        CountryAPIController mycontroller;
        Moq.Mock<ICountryService<CountryModel>> mock = new Moq.Mock<Interfaces.ICountryService<CountryModel>>();
        Moq.Mock<ILoggingService> logmock = new Moq.Mock<Interfaces.ILoggingService>();

        public CountryAPIControllerTests()
        {          
            mycontroller = new CountryAPIController(mock.Object, logmock.Object);
        }
        
        [TestMethod]
        public async Task Details_NoCountryFound_ReturnsEmptyCountry()
        {
            //Arrange
            CountryModel cm = new CountryModel() { CountryId = 0 };
            CountryModel lookingfor = new CountryModel() { CountryId = -1, Name = $"No country with id 0." };
            mock.Setup(x => x.GetCountry(cm.CountryId)).ReturnsAsync(lookingfor);            
            
            //Act
            var country = await mycontroller.GetCountry(cm);

            //Assert
            Assert.AreEqual(lookingfor.Name, country.Name);
        }

        [TestMethod]
        public async Task Details_NoCountryFound_ValidLogInformation()
        {
            //Arrange
            CountryModel cm = new CountryModel() { CountryId = 100 };
            CountryModel lookingfor = new CountryModel() { CountryId = -1, Name = $"No country with id {cm.CountryId}." };
            mock.Setup(x => x.GetCountry(cm.CountryId)).ReturnsAsync(lookingfor);

            //Act
            var country = await mycontroller.GetCountry(cm);

            //Assert
            logmock.Verify(x => x.LoggInfo($"No country with id {cm.CountryId}."));
        }

        [TestMethod]
        public async Task GetCountry_CountryFound_ReturnsValidCountry()
        {
            //Arrange
            CountryModel cm = new CountryModel() { CountryId = 1 };
            CountryModel lookingfor = new CountryModel() { CountryId = 1, Name = "Polska" };
            mock.Setup(x => x.GetCountry(cm.CountryId)).ReturnsAsync(lookingfor);
            
            //Act
            var country = await mycontroller.GetCountry(cm);

            //Assert
            Assert.AreEqual(lookingfor.Name, country.Name);
        }
    }
}
