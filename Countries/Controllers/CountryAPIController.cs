using Countries.Interfaces;
using Countries.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace Countries.Controllers
{

    /// <summary>
    /// API controller which calls directly service
    /// Every exception can be logged (e.i. in constructor can we add ILogger as parameter)
    /// Every known exception can have its own view....
    /// </summary>
    public class CountryAPIController : ApiController
    {
        protected ICountryService<CountryModel> _service;
        protected ILoggingService _logger;

        public CountryAPIController(ICountryService<CountryModel> service, ILoggingService logger)
        {
            _service = service;
            _logger = logger;
        }

        [Route("api/CountryAPI/GetCountries")]
        [HttpPost]
        public async Task<List<CountryModel>> GetCountries()
        {
            try
            {
                return await _service.GetCountries();
            }
            catch(Exception ex)
            {
                _logger.LoggError("Getting countries was not succesfull.", ex);
                throw;
            }

        }

        [Route("api/CountryAPI/GetCountry")]
        [HttpPost]
        public async Task<CountryModel> GetCountry(CountryModel country)
        {
            try
            {
                var c = await _service.GetCountry(country.CountryId);
                if (c.CountryId == -1) //no country found
                    _logger.LoggInfo(c.Name);
                return c;
            }
            catch (Exception ex)
            {
                _logger.LoggError("Getting country details was not succesfull.", ex);
                throw;
            }
        }

        [Route("api/CountryAPI/AddCountry")]
        [HttpPost]
        public async Task<bool> SaveCountry(CountryModel country)
        {
            try
            {
                return await _service.SaveCountry(country);
            }
            catch (Exception ex)
            {
                _logger.LoggError("Attempt to save country was not succesfull.", ex);
                throw;
            }
        }
    }
}