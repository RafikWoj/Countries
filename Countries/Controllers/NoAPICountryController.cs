using Countries.Interfaces;
using Countries.Models;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Countries.Controllers
{
    /// <summary>
    /// Controller which calls directly service
    /// Every exception can be logged (e.i. in constructor can we add ILogger as parameter)
    /// Every known exception can have its own view....
    /// Initially commented in the RouteConfig....
    /// </summary>
    public class NoAPICountryController : Controller
    {
        protected ICountryService<CountryModel> _service;
        protected ILoggingService _logger;

        public NoAPICountryController(ICountryService<CountryModel> service, ILoggingService logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<ActionResult> Countries()
        {
            try
            {
                List<CountryModel> countries = await _service.GetCountries();
                return View(countries);
            }
            catch(Exception ex)
            {
                _logger.LoggError("Getting countries was not succesfull.", ex);
                return View("Error"); 
            }
        }

        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                _logger.LoggError("Try to add new country was not succesfull.", ex);
                return View("Error"); 
            }
        }

        public async Task<ActionResult> Details(int countryId)
        {
            try
            {
                var country = await GetCountry(countryId);
                return View("Details", country);
            }
            catch(Exception ex)
            {
                _logger.LoggError("Getting country details was not succesfull.", ex);
                return View("Error"); 
            }
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                List<CountryModel> countries = await _service.GetCountries();
                return View("Countries", countries);
            }
            catch(Exception ex)
            {
                _logger.LoggError("Getting countries was not succesfull.", ex);
                return View("Error"); 
            }
        }

        private async Task<CountryModel> GetCountry(int countryid)
        {
            return await _service.GetCountry(countryid);
        }

        public async Task<ActionResult> SaveCountry(CountryModel country)
        {
            try
            {
                await _service.SaveCountry(country);

                List<CountryModel> countries = await _service.GetCountries();
                return View("Countries", countries);
            }
            catch(Exception ex)
            {
                _logger.LoggError("Attempt to save country was not succesfull.", ex);
                return View("Error"); 
            }
        }

    }
}